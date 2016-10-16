// JoiningTableData.cs
// Using LINQ to perform a join and aggregate data across tables.
using System;
using System.Linq;
using System.Windows.Forms;

namespace JoinQueries
{
    public partial class JoiningTableData : Form
    {
        public JoiningTableData()
        {
            InitializeComponent();
        } // end constructor

        private void JoiningTableData_Load(object sender, EventArgs e)
        {
            // Entity Framework DBContext
            BooksExamples.BooksEntities dbcontext =
               new BooksExamples.BooksEntities();

            // get authors and ISBNs of each book they co-authored
            var authorsAndTitles =
               from author in dbcontext.Authors
               from book in author.Titles
               orderby book.Title1, author.LastName
               select new {book.Title1, author.FirstName, author.LastName};

            outputTextBox.AppendText("Authors and Titles:");

            // display authors and ISBNs in tabular format
            foreach (var element in authorsAndTitles)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n\t{0,-10} {1} {2,-10}",
                      element.Title1, element.FirstName, element.LastName));
            } // end foreach

            // get authors and titles of each book they co-authored
            var authorsAndTitlesSortByAuthor =
               from book in dbcontext.Titles
               from author in book.Authors
               orderby book.Title1, author.LastName, author.FirstName
               select new
               {
                   author.FirstName,
                   author.LastName,
                   book.Title1
               };

            outputTextBox.AppendText("\r\n\r\nAuthors and titles with authors sorted for each title:");

            // display authors and titles in tabular format
            foreach (var element in authorsAndTitlesSortByAuthor)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n\t{0,-10} {1} {2, -10}",
                      element.Title1, element.FirstName, element.LastName));
            } // end foreach

            // get authors and titles of each book 
            // they co-authored; group by title
            var authorByTitles =
               from book in dbcontext.Titles
               orderby book.Title1
               select new
               {
                   Titles = book.Title1,
                   Writer = 
                    from author in book.Authors
                    orderby author.LastName, author.FirstName
                    select author.FirstName + " " + author.LastName
               };

            outputTextBox.AppendText("\r\n\r\nAuthors grouped by titles:");

            // display titles written by each author, grouped by title
            foreach (var titles in authorByTitles)
            {
                // display book's title
                outputTextBox.AppendText("\r\n\t" + titles.Titles + ":");

                // display titles written by that author
                foreach (var name in titles.Writer)
                {
                    outputTextBox.AppendText("\r\n\t\t" + name);
                } // end inner foreach
            } // end outer foreach
             
                        
               
        } // end method JoiningTableData_Load
    } // end class JoiningTableData
} // end namespace JoinQueries

