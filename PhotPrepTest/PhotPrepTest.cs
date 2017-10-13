using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotPrepForm;
using System.Threading.Tasks;

namespace PhotPrepTest
{
    [TestClass]
    public class PhotPrepTest
    {
        [TestMethod]
        public void GetTargetFileNamePostfixTest()
        {
            // arrange
            string sourceDir = @"c:\pictures\2017\01\2017_01_15\";
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
            string sourceDir = @"c:\pictures\2017\01\2017_01_15\";
            string targetDir = @"c:\Photometry\temp\";
            int expectedListCount = 4;
            PhotPrep prep = new PhotPrep();

            // act
            var fileList = await prep.CopyAllFiles(sourceDir, targetDir);
            int actualListCount = fileList.Count;

            //assert
            Assert.AreEqual<int>(expectedListCount, actualListCount, "Wrong list generation");
        }

        [TestMethod]
        public void ScriptLineTest()
        {
            //arrange
            string targetName = "vboo20170115p";
            int imgNumber = 4;
            string expectedScriptLine = "run preproc vboo20170115p 4 vboo20170115s 2";
            PhotPrep prep = new PhotPrep();

            //act
            var actualScriptLine = prep.GenerateScriptLine(targetName, imgNumber);

            //assert
            Assert.AreEqual(expectedScriptLine, actualScriptLine, "Wrong script line generation");
        }
    }
}
