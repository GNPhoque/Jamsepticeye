using System;

[Serializable]
public class RouletteList
{
	public int perfect = 1;
	public int ok = 3;
	public int cross = 2;

	public RouletteList(int p = 1, int o = 1, int c = 1)
	{
		perfect = p;
		ok = o;
		cross = c;
	}
}
