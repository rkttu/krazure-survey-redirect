<Query Kind="Statements" />

var baseDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

var dict = File.ReadAllText(Path.Combine(baseDir, "links.txt"))
	.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(x =>
	{
		var pair = x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		return new KeyValuePair<string, string>(pair[0], pair[1]);
	}).ToDictionary(x => x.Key, x => x.Value);
	
foreach (var eachKvp in dict)
{
	var targetPath = Path.Combine(baseDir, eachKvp.Key);
	if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
	
	File.WriteAllText(Path.Combine(targetPath, "index.html"), $@"<!DOCTYPE html>
<html>
    <head>
        <title>Korea Azure User Group Survey</title>
        <meta http-equiv=""refresh"" content=""0;url={eachKvp.Value}"" />
        <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
        <meta http-equiv=""Cache-Control"" content=""no-cache, no-store, must-revalidate"" />
        <meta http-equiv=""Pragma"" content=""no-cache"" />
        <meta http-equiv=""Expires"" content=""0"" />
        <meta charset=""UTF-8"" />
    </head>
    <body>
        <p>잠시만 기다려 주세요. 만약 자동으로 이동되지 않을 경우 아래 링크를 클릭하세요.</p>
        <a href=""{eachKvp.Value}"" target=""_self"">이동하기</a>
    </body>
</html>
", new UTF8Encoding(false));
}