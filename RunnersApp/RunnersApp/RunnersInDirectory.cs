using RunnersApp;
using static RunnersApp.RunnerBase;

namespace RunnersApp
{
    public class RunnersInDirectory
    {

        public delegate void RunnerAddedDelegate(object sender, EventArgs args);
        public event RunnerAddedDelegate RunnerAdded;
        private string fileName;
        public RunnersInDirectory()
        {

        }

        public void AddRunner(string name, string surname, string sex)
        {
            if (name.Length!=0 && surname.Length!=0 && sex.Length!=0) 
            {
                RunnerInFile runner = new RunnerInFile(name, surname, sex);
                fileName = sex + "_" + name + "_" + surname + ".txt";
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();

                    if (RunnerAdded != null)
                    {
                        RunnerAdded(this, new EventArgs());
                    }
                }
            }
            else
            {
                throw new Exception("invalid value");
            }
        }
        public void ShowRunners()
        {
            var dir = Directory.GetCurrentDirectory();
            List<string> files = new List<string>();
            int index = 1;
            string fullName = "";
            WritelineColor(ConsoleColor.Blue, "Actual list Runners in base: ");
            if (dir != null && Directory.GetFiles(dir,"*.txt").Count() > 0)
            {
                foreach (string file in Directory.GetFiles(dir))
                {

                    var ext = Path.GetExtension(file);
                    if (ext == ".txt")
                    {
                        fullName = Path.GetFileNameWithoutExtension(file);
                        files.Add(fullName);
                        Console.WriteLine(index + ". " + fullName.Split("_")[0] + " " + fullName.Split("_")[1] + " " + fullName.Split("_")[2]);
                        index++;
                    }
                }
            }
            else
            {
                throw new Exception("there are no files in the directory");
            }
        }
        public List<string> filesList
        {
            get
            {
                var dir = Directory.GetCurrentDirectory();
                List<string> filesList = new List<string>();
                string fullName = "";
                if (dir != null && Directory.GetFiles(dir, "*.txt").Count() > 0)
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        var ext = Path.GetExtension(file);
                        if (ext == ".txt")
                        {
                            fullName = Path.GetFileNameWithoutExtension(file);
                            filesList.Add(fullName);
                        }
                    }
                    return filesList;
                }
                else
                {
                    throw new Exception("there are no files in the directory");
                }
            }
        }
        private static void WritelineColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }



}