using System;
using System.Collections.Generic;
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
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace JobSeeker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Job> jobs = new List<Job>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> titleText = JobTitle.Text.Split(' ').ToList<string>();
            string jobUrlToSearch = "";
            foreach (string str in titleText)
                jobUrlToSearch = jobUrlToSearch + str +"-";

            jobUrlToSearch = jobUrlToSearch.Substring(0,jobUrlToSearch.Length-1)+"-jobs/";

            int i = 0;
            int j = 0;

            jobs.Add(new Job("--------------------------------- Jobs From Naukri ----------------------------"));
            j++;
            i++;
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.naukri.com/"+jobUrlToSearch);

            var urlElements = driver.FindElementsById("jdUrl");
            var titles = driver.FindElementsByClassName("desig");
            var locs = driver.FindElementsByClassName("loc");


            foreach (var title in titles)
            {
                jobs.Add(new Job(" Job Title : " + title.GetAttribute("title")));
            }
            foreach (var urlElement in urlElements)
            {
                jobs[i].url = urlElement.GetAttribute("href");
                i = i + 1;
            }
            foreach (var loc in locs)
            {
                jobs[j].location = "  \n Location : " + loc.Text;
                j = j + 1;
            }


            jobs.Add(new Job("--------------------------------- Jobs From Workindia ---------------------------------"));
            j++;
            i++;
            driver.Navigate().GoToUrl("https://www.workindia.in/software-developer-jobs-in-undefined/");
            //var input = driver.FindElementByCssSelector("input[class = 'SearchInput undefined']");
            //input.SendKeys("android developer"+"\n");
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            //IWebElement autocomplete = wait.Until(x => x.FindElement(By.CssSelector("div[class = 'JobDetail LocationDetail text-secondary']")));
            Thread.Sleep(20000);
            locs = driver.FindElementsByCssSelector("div[class = 'JobDetail LocationDetail text-secondary']");
            urlElements = driver.FindElementsByClassName("JobItem");
            titles = driver.FindElementsByClassName("JobItem");

            foreach (var title in titles)
            {
                jobs.Add(new Job(" Job Title : " + title.FindElement(By.TagName("a")).Text));
            }

            foreach (var urlElement in urlElements)
            {
                jobs[i].url = urlElement.FindElement(By.TagName("a")).GetAttribute("href");
                i = i + 1;
            }

            foreach (var loc in locs)
            {
                jobs[j].location = "  \n Location : " + loc.Text;
                j = j + 1;
            }

            JobsList.ItemsSource = jobs;
            driver.Close();
        }

        private void Jobs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(jobs[JobsList.SelectedIndex].url);
        }

        private void JobTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(JobTitle.Text.Equals("Enter Job Title"))
                JobTitle.Text = "";
        }
    }
}
