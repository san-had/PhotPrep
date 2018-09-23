using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotPrepForm;

namespace PhotPrepTest
{
    [TestClass]
    public class PhotPrepTest
    {
        [TestMethod]
        public void GetTargetFileNamePostfixTest()
        {
            // arrange

            string sourceDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Resources\\pictures\\2017\\01\\2017_01_15\\");
            string expectedPostfix = "20170115p";
            PhotPrep prep = new PhotPrep();

            // act
            string actualPostfix = prep.GetTargetFileNamePostfix(sourceDir);

            // assert
            Assert.AreEqual(expectedPostfix, actualPostfix, "Postfix is not correct!");
        }

        [TestMethod]
        public async Task ScriptTest()
        {
            //arrange
            string sourceDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Resources\\pictures\\2017\\01\\2017_01_15\\");
            string targetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Resources\\photometry\\temp\\");
            string blueChannelStarsConfiguration = "vvcep;uuaur";
            int expectedListCount = 4;
            PhotPrep prep = new PhotPrep();

            // act
            var fileList = await prep.CopyAllFiles(sourceDir, targetDir, blueChannelStarsConfiguration);
            int actualListCount = fileList.Count;

            //assert
            Assert.AreEqual<int>(expectedListCount, actualListCount, "Wrong list generation");
        }

        [TestMethod]
        public void ScriptLineTestWithNonBlue()
        {
            //arrange
            string targetName = "vboo20170115p";
            int imgNumber = 4;
            string expectedScriptLine = "run preproc vboo20170115p 4 vboo20170115s vboo20170115g blue";
            string[] blueChannelStars = new string[] { "vvcep", "uuaur" };
            PhotPrep prep = new PhotPrep();

            //act
            var actualScriptLine = prep.GenerateScriptLine(targetName, imgNumber, blueChannelStars);

            //assert
            Assert.AreEqual(expectedScriptLine, actualScriptLine, "Wrong script line generation");
        }

        [TestMethod]
        public void ScriptLineTestWithBlue()
        {
            //arrange
            string targetName = "vvcep20170115p";
            int imgNumber = 4;
            string expectedScriptLine = "run preproc vvcep20170115p 4 vvcep20170115s vvcep20170115g vvcep20170115b";
            string[] blueChannelStars = new string[] { "vvcep", "uuaur" };
            PhotPrep prep = new PhotPrep();

            //act
            var actualScriptLine = prep.GenerateScriptLine(targetName, imgNumber, blueChannelStars);

            //assert
            Assert.AreEqual(expectedScriptLine, actualScriptLine, "Wrong script line generation");
        }

        [TestMethod]
        public void IsBlueChannelStarTestWithBlueChannelStar()
        {
            //arrange
            string targetName = "vvcep20170115p";
            string[] blueChannelStars = new string[] { "vvcep", "uuaur" };
            PhotPrep prep = new PhotPrep();
            bool expectedResult = true;

            //act
            var actualResult = prep.IsBlueChannel(targetName, blueChannelStars);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void IsBlueChannelStarTestWithNonBlueChannelStar()
        {
            //arrange
            string targetName = "vboo20170115p";
            string[] blueChannelStars = new string[] { "vvcep", "uuaur" };
            PhotPrep prep = new PhotPrep();
            bool expectedResult = false;

            //act
            var actualResult = prep.IsBlueChannel(targetName, blueChannelStars);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}