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
        private Project _curproj;
        TaskProject _task;
        int _curtask;
        private int _currentPage = 1;
        private int _countOfItems = 5;
        private int _numberOfPages;
        private int _minusPage = 0;

        private List<UX.Entity.Document> _document;
        private List<UX.Entity.Document> _filterdocument;

        public PageDocs(Project _selectedProj, TaskProject _selectedTask)
        {
            InitializeComponent();
            

            if(_selectedProj != null)
            {
                _curproj = _selectedProj;
                DataContext = _selectedProj;
                _document = App.DataBase.Documents.Where(p => p.ProjectID == _curproj.ProjectID).ToList();
            }
            if(_selectedTask != null)
            {
                _task = _selectedTask;
                DataContext = _selectedTask;
                _document = App.DataBase.Documents.Where(p => p.TaskID == _task.TaskID).ToList();
            }
            DGDocs.ItemsSource = _document;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                NavigationChange();
                DataGridUpdate();
            }
        }

        private void NavigationChange()
        {
            int count = 0;
            int page = 0;
            pageList.Items.Clear();
            count = _document.Count;
            if (count % _countOfItems == 0)
                _numberOfPages = count / _countOfItems;
            else
                _numberOfPages = count / _countOfItems + 1;
            for (int i = 1; i <= _numberOfPages; i++)
            {
                page = i;
                pageList.Items.Add(page.ToString());
                page = page + 1;

            }
        }
        private void DataGridUpdate()
        {
            _filterdocument = _document;
            
                if (_task != null)
                {
                    _filterdocument = App.DataBase.Documents.Where(p => p.TaskID == _task.TaskID).ToList();
                }
                if (_curproj != null)
                {
                    _filterdocument = App.DataBase.Documents.Where(p => p.ProjectID == _curproj.ProjectID).ToList();
                } 
                
            _filterdocument = _filterdocument.Where(p => p.DocumentName.ToUpper().Contains(txtFilter.Text.ToUpper())).ToList();

            _document = _filterdocument;
            DGDocs.ItemsSource = _filterdocument.Take(_countOfItems).ToList();
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 0)
            {
                _minusPage = _currentPage - 1;
                DGDocs.ItemsSource = _document.Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();
                _currentPage--;
            }
            NavigationChange();
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _numberOfPages)
            {
                DGDocs.ItemsSource = _document.Skip(_countOfItems * _currentPage).Take(_countOfItems).ToList();
                _currentPage++;
            }
            NavigationChange();
        }

        private void btnDownloadDocs_Click(object sender, RoutedEventArgs e)
        {
            var path = "";
            Document data = ((FrameworkElement)sender).DataContext as Document;
            List<Document> files = new List<Document>();
            using (SqlConnection conn = new SqlConnection(ClassConnect.GetSQLConnString()))
            {
                conn.Open();
                string query = "select * from Document where DocumentID = '" + data.DocumentID + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Guid fileId = reader.GetGuid(0);
                    byte[] fileData = (byte[])reader.GetValue(1);
                    string project = (string)reader.GetValue(2);
                    string fileName = (string)reader.GetString(3);
                    int task = reader.GetInt32(4);
                    Document file = new Document(fileId, fileName, fileData, project, task);
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
            if(path != null)
            {
                using (FileStream fs = new FileStream(path.ToString(), FileMode.OpenOrCreate))
                {
                    fs.Write(files[0].FileData, 0, files[0].FileData.Length);

                    MessageBox.Show("Файл успешно сохранён");

                    
                }
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
              "INSERT INTO Document ([DocumentID], [DocumentName], [ProjectID], [TaskID]) " +
              "VALUES (@FileId, @FileName, @Project, @Task)", conn);
            insert.Parameters.Add("@FileId",
              SqlDbType.UniqueIdentifier).Value = fileId;
            insert.Parameters.Add("@FileName",
              SqlDbType.VarChar, 50).Value = fileName;
            if(_curproj != null)
            insert.Parameters.Add("@Project",
              SqlDbType.Int).Value = _curproj.ProjectID;
            if(_task != null)
            {
                insert.Parameters.Add("@Project",
                SqlDbType.Int).Value = _task.ProjectID;
                insert.Parameters.Add("@Task",
                SqlDbType.Int).Value = _task.TaskID;
            }
                
            insert.ExecuteNonQuery();

            SqlTransaction fsTx = conn.BeginTransaction();
            SqlCommand getTransaction = new SqlCommand(
              "SELECT [DocumentSource].PathName(), " +
              "GET_FILESTREAM_TRANSACTION_CONTEXT() " +
              "FROM Document " +
              "WHERE DocumentID = @FileID", conn);
            getTransaction.Transaction = fsTx;
            getTransaction.Parameters.Add("@FileId",
              SqlDbType.UniqueIdentifier).Value = fileId;

            SqlDataReader contextReader = getTransaction.ExecuteReader(CommandBehavior.SingleRow);
            contextReader.Read();
            string filePath = contextReader.GetString(0);
            byte[] transactionId = (byte[])contextReader[1];
            contextReader.Close();
            try
            {
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
                            MessageBox.Show("Сохранение прошло успешно");
                        }
                    }
                }
                fsTx.Commit();
                conn.Close();
                DGDocs.ItemsSource = _document;
            }
            catch
            {
                MessageBox.Show("Путь не может быть пустым. Пожалуйста, попробуйте ещё раз");
            }
           
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridUpdate();
            NavigationChange();
        }

        private void UCPageBtn_ButtonClick(object sender, RoutedEventArgs e)
        {
            _currentPage = (int)sender;
            _minusPage = _currentPage - 1;
            if (_currentPage > 1)
            {
                DGDocs.ItemsSource = _document.ToList().Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();

            }
            else
            {
                DGDocs.ItemsSource = _document.ToList().Take(_countOfItems).ToList();
            }
            NavigationChange();
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if(ClassNavigate.NavigateFrame.CanGoBack)
            ClassNavigate.NavigateFrame.GoBack();
        }

        private void btnDeleteDocs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var remove = DGDocs.SelectedItems.Cast<Document>().ToList();
                App.DataBase.Documents.RemoveRange(remove);
                App.DataBase.SaveChanges();
                DataGridUpdate();
            }
            catch
            {
                MessageBox.Show("Ошибка удаления");
            }
        }
    }
}
