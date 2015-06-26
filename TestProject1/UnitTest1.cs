using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefectTracking;

namespace TestProject1
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //Name=ctl00$m$g_dcef8416_a54d_42d7_a33e_3ac0ad4549a5$ctl00$ckiRegNewContractUc$bcDeelnemer$bcTextBox
            string ori = "Name=ctl00$m$g_dcef8416_a54d_42d7_a33e_3ac0ad4549a5$ctl00$ckiRegNewContractUc$bcDeelnemer$bcTextBox<";
            string changed = "Name=*$ckiRegNewContractUc$bcDeelnemer$bcTextBox<";
            Assert.AreEqual(changed, Helper.RemoveWildCard(ori));
        }

        [TestMethod]
        public void TestMethod2()
        {
            //ID=ctl00_m_g_dcef8416_a54d_42d7_a33e_3ac0ad4549a5_ctl00_chkKeuzeVasthouden<
            string ori = "ID=ctl00_m_g_dcef8416_a54d_42d7_a33e_3ac0ad4549a5_ctl00_chkKeuzeVasthouden<";
            string changed = "ID=*_chkKeuzeVasthouden<";
            Assert.AreEqual(changed, Helper.RemoveWildCard(ori));
        }
    }
}
