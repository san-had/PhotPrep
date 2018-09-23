namespace PhotPrepForm
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class PhotPrep
    {
        public PhotPrep()
        {
        }

        public async Task<List<string>> CopyAllFiles(string sourceDir, string targetDir, string blueChannelStarsConfiguration)
        {
            var scriptList = new List<string>();

            var targetNamePostfix = GetTargetFileNamePostfix(sourceDir);

            var directoryList = Directory.EnumerateDirectories(sourceDir)
                                         .Where(x => !x.Contains("dark"));

            foreach (var dir in directoryList)
            {
                var targetName = new DirectoryInfo(dir).Name.ToLower().Replace("_", string.Empty) + targetNamePostfix;
                var files = Directory.EnumerateFiles(dir);
                var index = 0;
                bool isFileExist = false;
                foreach (var filename in files)
                {
                    FileStream sourceStream;
                    using (sourceStream = File.Open(filename, FileMode.Open))
                    {
                        index++;
                        var fileOriginalName = filename;
                        var fileTargetName = targetDir + targetName + index + ".cr2";
                        isFileExist = File.Exists(fileTargetName);
                        if (!isFileExist)
                        {
                            using (FileStream destinationStream = File.Create(fileTargetName))
                            {
                                await sourceStream.CopyToAsync(destinationStream);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (!isFileExist)
                {
                    var blueChannelStars = blueChannelStarsConfiguration.Split(';');
                    scriptList.Add(GenerateScriptLine(targetName, index, blueChannelStars));
                }
                else
                {
                    scriptList.Add("Files (" + targetName + " are already copied!");
                }
            }
            return scriptList;
        }

        public string GetTargetFileNamePostfix(string sourceDir)
        {
            string postfix = string.Empty;

            var directory = new DirectoryInfo(sourceDir);

            var date = directory.Name.Replace("_", string.Empty);

            postfix = date + "p";

            return postfix;
        }

        public string GenerateScriptLine(string targetName, int imgNumber, string[] blueChannelStars)
        {
            var rootTargetName = targetName.Remove(targetName.Length - 1);

            var targetNameReSized = $"{rootTargetName}s";

            var targetNameGreen = $"{rootTargetName}g";

            bool isBlueChannelStar = IsBlueChannel(targetName, blueChannelStars);

            var tagetNameBlue = isBlueChannelStar ? $"{rootTargetName}b" : "blue";

            string scriptLine = $"run preproc {targetName} {imgNumber} {targetNameReSized} {targetNameGreen} {tagetNameBlue}";

            return scriptLine;
        }

        public bool IsBlueChannel(string targetName, string[] blueChannelStars)
        {
            if (blueChannelStars == null || blueChannelStars.Length == 0)
            {
                return false;
            }

            var isBlue = blueChannelStars.Any(x => targetName.StartsWith(x));

            return isBlue;
        }
    }
}