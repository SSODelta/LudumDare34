using UnityEngine;
using System.Collections;

public class Command{

	private string cmd;

	public Command(){
		cmd = "";
	}

	public void addRight(){
		cmd += "r";
	}

	public void addLeft(){
		cmd += "l";
	}

	public string getCmd(){
		return cmd;
	}

	public bool empty(){
		return cmd.Length == 0;
	}

	public int len(){
		return cmd.Length;
	}

	public bool isCmd(string str){
		return cmd.Equals(str.ToLower());
	}
}
