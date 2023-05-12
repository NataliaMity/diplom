using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCheck.Tests
{
    [TestClass()]
    public class ClientCheckerTests
    {
        [TestMethod()]
        public void Check_AllTrue()
        {
            string name = "ООО 'Оператор'";
            string chef = "Оленин Михаил Петрович";
            string post = "Директор";
            string email = "director@gmail.com";
            string inn = "101912181929";

            bool expected = true;
            bool actual = LibraryCheck.ClientChecker.ClientCheck(name, chef, post, email, inn);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_NameInt_False()
        {
            string name = "ООО Оператор123";
            string chef = "Оленин Михаил Петрович";
            string post = "Директор";
            string email = "director@gmail.com";
            string inn = "101912181929";

            bool expected = false;
            bool actual = LibraryCheck.ClientChecker.ClientCheck(name, chef, post, email, inn);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_INNChar_False()
        {
            string name = "ООО Оператор";
            string chef = "Оленин Михаил Петрович";
            string post = "Директор";
            string email = "director@gmail.com";
            string inn = "newinn";

            bool expected = false;
            bool actual = LibraryCheck.ClientChecker.ClientCheck(name, chef, post, email, inn);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_ShortINN_False()
        {
            string name = "ООО Оператор";
            string chef = "Оленин Михаил Петрович";
            string post = "Директор";
            string email = "director@gmail.com";
            string inn = "105";

            bool expected = false;
            bool actual = LibraryCheck.ClientChecker.ClientCheck(name, chef, post, email, inn);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_EmailWrong_False()
        {
            string name = "ООО Оператор";
            string chef = "Оленин Михаил Петрович";
            string post = "Директор";
            string email = "director @ gmail.com";
            string inn = "101912181929";

            bool expected = false;
            bool actual = LibraryCheck.ClientChecker.ClientCheck(name, chef, post, email, inn);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_EmptySpaces_False()
        {
            string name = "";
            string chef = "";
            string post = "";
            string email = "";
            string inn = "";

            bool expected = false;
            bool actual = LibraryCheck.ClientChecker.ClientCheck(name, chef, post, email, inn);

            Assert.AreEqual(expected, actual);
        }
    }
}