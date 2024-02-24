using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace BaykarTestCase
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NavbarTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://kariyer.baykartech.com/tr/");

            try
            {
                
                var navbarElements = driver.FindElements(By.CssSelector(".nav.navbar-nav.navbar-right > li"));

                
                foreach (var navbarElement in navbarElements)
                {
                    navbarElement.Click();
                    Thread.Sleep(2000);

                    
                    var dropdownMenuItems = navbarElement.FindElements(By.CssSelector(".dropdown-menu > li > a"));

                    
                    foreach (var menuItem in dropdownMenuItems)
                    {
                        menuItem.Click();
                        Thread.Sleep(2000);
                        
                        WebDriverWait newPageWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        newPageWait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                        
                        if (!IsDropdownMenuDisplayed(navbarElement))
                        {
                            driver.Navigate().GoToUrl("https://kariyer.baykartech.com/tr/");
                            navbarElement.Click();
                            Thread.Sleep(2000);
                            dropdownMenuItems = navbarElement.FindElements(By.CssSelector(".dropdown-menu > li > a"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // WebDriver'ý kapatma
                driver.Quit();
            }
        }

        [Test]
        public void LanguageTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://kariyer.baykartech.com/tr/");
            try
            {
                IWebElement langButton = driver.FindElement(By.CssSelector(".siteCustomLi > a"));
                langButton.Click();

                var url = driver.Url;
                if (url.Contains("en"))
                {
                    Console.WriteLine("Dil deðiþtirme iþlemi baþarýlý");
                }
                else
                {
                    Console.WriteLine("Dil deðiþtirme iþlemi baþarýsýz");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        [Test]
        public void CarrierTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://kariyer.baykartech.com/tr/");
            try
            {
                IWebElement openPositions = driver.FindElement(By.CssSelector(".fix-btn"));
                openPositions.Click();

                // Search Positions
                IWebElement searchPosition = driver.FindElement(By.CssSelector("#myInput"));
                searchPosition.SendKeys("Test");
                Thread.Sleep(1000);

                var positions = driver.FindElements(By.CssSelector(".position-head > a"));
                var isSearchingTrue = false;
                foreach (var position in positions)
                {
                    if(position.Text.Contains("Test"))
                    {
                        isSearchingTrue = true;
                    }
                    else
                    {
                        isSearchingTrue &= false;
                        break;
                    }
                }
                Console.WriteLine(isSearchingTrue);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Dropdown menünün görünürlüðünü kontrol eden yardýmcý bir metod
        private bool IsDropdownMenuDisplayed(IWebElement navbarElement)
        {
            try
            {
                // Dropdown menü öðesini bulma
                var dropdownMenu = navbarElement.FindElement(By.CssSelector(".dropdown-menu"));
                return dropdownMenu.Displayed;
            }
            catch (NoSuchElementException)
            {
                // Eðer dropdown menü bulunamazsa veya görüntülenemezse, false döndür
                return false;
            }
        }

        
    }
}