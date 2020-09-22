using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;

namespace Binger
{
	internal static class DuplicateHandler
	{
		private static readonly List<string> Extensions = new List<string>(new[] { ".jpg", ".jpeg", ".jif", ".jfif", ".jfi", ".png", ".bmp" });

		public static void RemoveDuplicates(string folder)
		{
			var delPath = Path.Combine(folder, "del.cmd");

			if (File.Exists(delPath)) { File.Delete(delPath); }

			var allImageFiles = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);

			var filesToProcess = allImageFiles.Where(file => Extensions.Contains(Path.GetExtension(file).ToLower())).ToList();

			var magicFiles = filesToProcess.Select(file => new MagickImage(file)).ToList();

			var groups = magicFiles.GroupBy(file => file.GetHashCode()).ToList();

			Parallel.ForEach(groups.Where(g => g.Count() > 1), group =>
			{
				var files = group.OrderBy(file => file.FileName.Length).ToList();

				if (files.Count <= 1) { return; }

				var fileToKeep = files.FirstOrDefault(file => file.FileName.Contains("auto") || file.FileName.Contains("in") || file.FileName.Contains("us") || file.FileName.Contains("uk"));
				// File Name found
				if (fileToKeep == null) { fileToKeep = files[0]; }

				foreach (var file in files.Where(file => !file.FileName.Equals(fileToKeep.FileName, StringComparison.OrdinalIgnoreCase)))
				{
					//NativeMethods.DeleteCompletelySilent(file.FileName);
					NativeMethods.MoveToRecycleBin(file.FileName);
				}
			});

			//var processedFiles = new List<MagickImage>();

			//var groups = new Dictionary<string, List<MagickImage>>();


			//foreach (var firstFile in magicFiles)
			//{
			//	// File Already Processed, continue to next one
			//	if (processedFiles.Contains(firstFile)) { continue; }

			//	var fileName = Path.GetFileName(firstFile.FileName);

			//	var key = Path.GetFileNameWithoutExtension(fileName).Split(' ')[0];

			//	// Add file to processed List
			//	processedFiles.Add(firstFile);

			//	// Loop on all non processed files
			//	foreach (var secondFile in magicFiles.Where(secondFile => !firstFile.FileName.Equals(secondFile.FileName, StringComparison.OrdinalIgnoreCase)))
			//	{
			//		// File Already Processed, continue to next one
			//		if (processedFiles.Contains(secondFile)) { continue; }

			//		var similarity = firstFile.Compare(secondFile, ErrorMetric.NormalizedCrossCorrelation) * 100;
			//		// if more than 90% similar, it is a duplicate
			//		if (!(similarity > 90d)) { continue; }

			//		if (!groups.ContainsKey(key)) { groups.Add(key, new List<MagickImage>()); }

			//		groups[key].Add(secondFile);
			//		// Add to processed file as not required to be processed again
			//		processedFiles.Add(secondFile);
			//	}
			//}

			//foreach (var fileName in groups.Where(@group => @group.Value.Count > 1).SelectMany(@group => @group.Value))
			//{
			//	File.AppendAllLines(Path.Combine(folder, "del.cmd"), new[] { "Hash : " + fileName.GetHashCode() + "\tFileName:" + fileName.FileName });
			//	//File.Delete(fileName.FileName);
			//}

			Parallel.ForEach(magicFiles, file => { file.Dispose(); });

			//foreach (var file in magicFiles)
			//{
			//	file.Dispose();
			//}
		}
	}
}
