using MityaginaNP.UX.Entity;
using MityaginaNP.UX.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MityaginaNP.UI.Window;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MityaginaNP.UI.Page
{
    /// <summary>
    /// Логика взаимодействия для PageDocs.xaml
    /// </summary>
    public partial class PageDocs 
    {
        FileSample _curfile;
        public PageDocs()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                DGDocs.ItemsSource = App.FSBase.FileSamples.ToList();
            }
        }

        private void btnShowDocs_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(ClassConnect.GetSQLConnString());
            conn.Open();

            string strQuery = "select Cast(FileData as varchar(MAX)) from FileSamples";

            SqlCommand cmd = new SqlCommand(strQuery, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                test.Text = String.Format("{0}", reader[0]);

            }
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDownloadDocs_Click(object sender, RoutedEventArgs e)
        {
            var path = "";
            FileSample data = ((FrameworkElement)sender).DataContext as FileSample;
            List<FileSample> files = new List<FileSample>();
            using (SqlConnection conn = new SqlConnection(ClassConnect.GetSQLConnString()))
            {
                conn.Open();
                string query = "select * from FileSamples where FileId = '" + data.FileId +"'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Guid fileId = reader.GetGuid(0);
                    string fileName = (string)reader.GetString(1);
                    byte[] fileData = (byte[])reader.GetValue(2);
                    FileSample file = new FileSample(fileId, fileName, fileData);
                    files.Add(file);
                }
            }
            var fd = new CommonOpenFileDialog();
            fd.Title = "Сохранить в...";
            fd.IsFolderPicker = true;

            fd.AddToMostRecentlyUsedList = false;
            
            fd.AllowNonFileSystemItems = false;
            fd.EnsureFileExists = true;
            fd.EnsurePathExists = true;
            fd.EnsureReadOnly = false;
            fd.EnsureValidNames = true;
            fd.Multiselect = false;
            fd.ShowPlacesList = true;
            fd.IsFolderPicker = true;
            if(fd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                path = fd.FileName + "\\" + files[0].FileName;
            }
            using (FileStream fs = new FileStream(path.ToString(), FileMode.OpenOrCreate))
                {
                    fs.Write(files[0].FileData, 0, files[0].FileData.Length);
                    
                    MessageBox.Show("файл скачан");
                }
        }

        private void BtnAddDoc_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            string path = fd.FileName;
            string fileName = fd.SafeFileName;


            Guid fileId = Guid.NewGuid();
            SqlConnection conn = new SqlConnection(ClassConnect.GetSQLConnString());

            conn.Open();
            SqlCommand insert = new SqlCommand(
              "INSERT INTO FileSamples ([FileId], [FileName]) " +
              "VALUES (@FileId, @FileName)", conn);
            insert.Parameters.Add("@FileId",
              SqlDbType.UniqueIdentifier).Value = fileId;
            insert.Parameters.Add("@FileName",
              SqlDbType.VarChar, 50).Value = fileName;
            insert.ExecuteNonQuery();

            SqlTransaction fsTx = conn.BeginTransaction();
            SqlCommand getTransaction = new SqlCommand(
              "SELECT [FileData].PathName(), " +
              "GET_FILESTREAM_TRANSACTION_CONTEXT() " +
              "FROM FileSamples " +
              "WHERE FileId = @FileID", conn);
            getTransaction.Transaction = fsTx;
            getTransaction.Parameters.Add("@FileId",
              SqlDbType.UniqueIdentifier).Value = fileId;

            SqlDataReader contextReader = getTransaction.ExecuteReader(CommandBehavior.SingleRow);
            contextReader.Read();
            string filePath = contextReader.GetString(0);
            byte[] transactionId = (byte[])contextReader[1];
            contextReader.Close();

            using (FileStream fs = File.OpenRead(path))
            {
                using (SqlFileStream sqlFS = new SqlFileStream(filePath, transactionId, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int location = fs.Read(buffer, 0, buffer.Length);
                    while (location > 0)
                    {
                        sqlFS.Write(buffer, 0, location);
                        location = fs.Read(buffer, 0, buffer.Length);
                    }
                }
            }
            fsTx.Commit();
            conn.Close();
        }
    }
}
