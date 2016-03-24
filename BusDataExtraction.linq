<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Extensions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Services.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Activation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Services.Client.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Entity.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Web.Script.Serialization</Namespace>
</Query>

void Main()
{
	var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BangkokBus");
	var text = File.ReadAllText(Path.Combine(path, "BusRoutesCleanTable.txt"));
	
	// Split text
	var lines = new List<Line>();
	var rows = text.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
	foreach(var r in rows)
	{
		var cols = r.Split('\t');
		var line = new Line();
		line.id = cols[0];
		
		// Split up the 
		line.directions = ParseDirections(cols[1].Trim('"', ' ').Split('\n'));
		
		lines.Add(line);
	}
	
	lines.Dump("lines");
	
	var ser = new JavaScriptSerializer();
	var json = ser.Serialize(lines);
	File.WriteAllText(Path.Combine(path, "BusFinder\BusFinder\BusData.js"), "BusData = " + json + ";"); 
}

List<LineDirection> ParseDirections(string[] stations)
{
	LineDirection curDir = null;
	var dirs = new List<LineDirection>();
	foreach(var s in stations) {
		var st = s.Trim();
		if (st == "") { continue; }
		// header
		if (st.StartsWith("*")) {
			if (curDir != null) {
				dirs.Add(curDir);
				curDir = null;
			}
			curDir = new LineDirection(st.Trim('*'));
			continue;
		}
		// station
		if (curDir == null) {
			curDir = new LineDirection("");
		}
		curDir.stations.Add(st);		
	}
	if (curDir != null) {
		dirs.Add(curDir);
		curDir = null;
	}
	
	return dirs;
}


class Line {
	public string id;
	public List<LineDirection> directions;
}

class LineDirection {
	public LineDirection(string dir) {
		this.direction = dir;
	}
	public string direction;
	public List<string> stations = new List<string>();
}