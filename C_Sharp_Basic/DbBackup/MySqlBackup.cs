using System.Diagnostics;

namespace Basic.DbBackup
{
	public class MySqlBackup
	{
		private static Timer backupTimer;

		//static void Main()
		//{
		//	// Schedule backup every day at 2 AM
		//	//ScheduleBackup();
		//	Console.WriteLine("MySQL Backup Service is running...");
		//	Console.ReadLine();
		//}

		//static void ScheduleBackup()
		//{
		//	DateTime now = DateTime.Now;
		//	DateTime scheduledTime = DateTime.Today.AddHours(2); // 2 AM

		//	if (now > scheduledTime)
		//		scheduledTime = scheduledTime.AddDays(1); // Schedule for the next day if time has passed

		//	double timeToGo = (scheduledTime - now).TotalMilliseconds;

		//	backupTimer = new Timer(timeToGo);
		//	backupTimer.Elapsed += (sender, e) =>
		//	{
		//		PerformBackup();
		//		backupTimer.Interval = TimeSpan.FromDays(1).TotalMilliseconds; // Repeat daily
		//	};
		//	backupTimer.Start();
		//}

		public static void PerformBackup()
		{
			string user = "root";
			string password = "root";
			string database = "kakehashi";
			string backupDir = "/path/to/backup"; // Use C:\\path\\to\\backup on Windows
			string date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
			string backupFile = Path.Combine(backupDir, $"{database}_backup_{date}.sql");

			Directory.CreateDirectory(backupDir); // Ensure backup directory exists

			string dumpCommand = $"mysqldump -u {user} -p{password} {database}";


			// Include --routines to back up stored procedures and functions
			//string dumpCommand = $"mysqldump -u {user} -p{password} --routines --databases {database}";

			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "cmd.exe", // Use "cmd.exe" on Windows and bash or linux
					Arguments = $"/C \"{dumpCommand} > {backupFile}\"", // For Windows: /C "mysqldump..." and for Linux -c "mysqldump..."
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					UseShellExecute = false,
					CreateNoWindow = true
				}
			};

			process.Start();
			string output = process.StandardOutput.ReadToEnd();
			string error = process.StandardError.ReadToEnd();
			process.WaitForExit();

			if (process.ExitCode == 0)
			{
				Console.WriteLine($"Backup successful: {backupFile}");
			}
			else
			{
				Console.WriteLine("Backup failed:");
				Console.WriteLine(error);
			}
		}
	}
}
