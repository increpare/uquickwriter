﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class QuickScript : MonoBehaviour {
	[TextArea(3,10)]
	public string inputString="";

	public Texture2D tex;
	public UnityEngine.UI.RawImage rawImage;

	public struct Character {
		public string c; //character
		public int m; //multiplicity
		public string variant;
		public Character(string c){
			if (c=="w"){
				this.c="u";
				this.m=2;
			} else {
				this.c=c;
				this.m=1;
			}
			this.variant=this.c;
		}

		public bool subO(){
			return "aceo".IndexOf(c)>=0;
		}
		
		public bool noLow(){
			return "acehijklmnorstuvz1234567890".IndexOf (c) >= 0 || variant == "x_raised";
		}
		
		public bool noLowLeft(){
			return "acehijklmnoqrstuvz1234567890".IndexOf (c) >= 0;
		}

		public bool noLowRight(){
			return "acehijklmnoprstuvyz1234567890".IndexOf (c) >= 0;
		}
		
		public bool groundNookLeft(){
			return "dghnq".IndexOf (c) >= 0;
		}

		public bool groundNookRight(){
			return "bprty".IndexOf (c) >= 0;
        }
        
    }
    
	struct SquidgeDat {
		public string a;
		public string b;
		public int dx;
		public SquidgeDat(string a,string b, int dx){
            this.a=a;
            this.b=b;
			this.dx=dx;
		}
	}

	private Dictionary<string,List<string[]>> variantDict;
	private string[][] variantselect = new string[][]{
		new string[]{"c","d","c_flatlow","d_raised"},
		new string[]{"c","l","c_flatlonglow","l"},
		new string[]{"c","j","c_flatlonglow","j"},
		new string[]{"c","a","c_flatlonghigh","a_flat"},
		new string[]{"c","h","c_flatlonghigh","h"},
		new string[]{"c","u","c_flatlonghigh","u"},
		new string[]{"c","v","c_flathigh","v"},
		new string[]{"c","m","c_flatlow","m_long"},
		new string[]{"c","n","c_flathigh","n_long"},
		new string[]{"e","l","e_longlow","l"},
		new string[]{"m","n","m_long","n_long"},
		new string[]{"n","m","n_long","m_long"},
		new string[]{"n","t","n_long","t"},
		new string[]{"n","j","n_long","j"},
		new string[]{"m","a","m_long","a_flat"},
		new string[]{"e","m","e","m_long"},
		new string[]{"e","r","e_longlow","r"},
		new string[]{"m","g","m_long","g_flatupper"},
		new string[]{"n","g","n_long","g_flatupper"},
		new string[]{"r","o","r_long","o"},
		new string[]{"n","s","n_long","s"},
		new string[]{"u","s","u","s_shortlow"},
		new string[]{"v","s","v","s_shortlow"},
		new string[]{"a","g","a","g_shortflatupper"},
		new string[]{"e","g","e","g_shortflatupper"},
		new string[]{"o","g","o","g_flatupper"},
		new string[]{"o","m","o","m_long"},
		new string[]{"m","o","m_long","o"},
		new string[]{"o","n","o","n_long"},
		new string[]{"n","o","n_long","o"},
		new string[]{"u","g","u","g_shortflatupper"},
		new string[]{"v","g","v","g_shortflatupper"},
		new string[]{"t","a","t","a_flat"},
		new string[]{"i","n","i","n_long"},
		new string[]{"m","z","m_long","z"},
		new string[]{"z","m","z","m_long"},
		new string[]{"m","x","m_long","x"},
		new string[]{"x","n","x","n_long"},
		new string[]{"i","m","i","m_long"},
		new string[]{"a","q","a","q_high"},
		new string[]{"b","f","b_raised","f_lowered"},
		new string[]{"b","g","b_raised","g_longlow"},
		new string[]{"b","x","b","x_raised"},
		new string[]{"c","g","c_flathigh","g_flatupper"},
		new string[]{"c","i","c_flatlonghigh","i"},
		new string[]{"c","q","c_flathigh","q"},
		new string[]{"c","r","c_flatlow","r"},
		new string[]{"c","s","c_flatlonghigh","s_shortlow"},
		new string[]{"c","t","c_flatlonglow","t"},
		new string[]{"c","x","c_flatlonghigh","x"},
		new string[]{"c","z","c_flatlonghigh","z"},
		new string[]{"e","j","e_longlow","j"},
		new string[]{"e","t","e_longlow","t"},
		new string[]{"h","g","h","g_shortupper"},
		new string[]{"h","m","h","m_long"},
		new string[]{"h","q","h","q_high"},
		new string[]{"j","g","j","g_shortupper"},
		new string[]{"l","g","l","g_shortupper"},
		new string[]{"l","m","l","m_long"},
		new string[]{"j","n","j","n_long"},
		new string[]{"j","s","j","s_shortlow"},
		new string[]{"j","x","j","x_wide"},
		new string[]{"m","c","m_long","c"},
		new string[]{"m","h","m_long","h"},
		new string[]{"m","i","m_long","i"},
		new string[]{"m","p","m_long","p"},
		new string[]{"m","s","m_long","s"},
		new string[]{"m","u","m_long","u"},
		new string[]{"m","v","m_long","v"},
		new string[]{"m","j","m_long","j"},
		new string[]{"n","l","n_long","l"},
		new string[]{"n","r","n_long","r"},
		new string[]{"p","a","p_long","a_flat"},
		new string[]{"p","g","p_long","g_shortupper"},
		new string[]{"p","j","p_long","j"},
		new string[]{"p","n","p","n_long"},
		new string[]{"p","o","p_long","o"},
		new string[]{"r","a","r","a_flat"},
		new string[]{"r","q","r","q_high"},
		new string[]{"m","q","m","q_high"},
		new string[]{"r","g","r","g_shortupper"},
		new string[]{"r","n","r","n_long"},
		new string[]{"s","g","s","g_shortupper"},
		new string[]{"s","q","s","q"},
		new string[]{"t","g","t","g_shortupper"},
		new string[]{"t","n","t","n_long"},
		new string[]{"t","q","t","q_high"},
		new string[]{"u","q","u","q_high"},
		new string[]{"u","n","u","n_long"},
		new string[]{"u","x","u","x_wide"},
		new string[]{"v","n","v","n_long"},
		new string[]{"v","q","v","q_high"},
		new string[]{"x","a","x","a_flatlong"},
		new string[]{"x","d","x_raised","d"},
		new string[]{"x","g","x_wide","g_flatupper"},
		new string[]{"x","q","x","q_high"},
		new string[]{"x","s","x","s_shortlow"},
		new string[]{"x","z","x_wide","z"},
		new string[]{"x","u","x_wide","u"},
		new string[]{"x","u","x_wide","u"},
		new string[]{"y","a","y_raised","a"},
		new string[]{"y","c","y_raised","c"},
		new string[]{"y","e","y_raised","e"},
		new string[]{"y","g","y_raised","g"},
		new string[]{"y","i","y_raised","i"},
		new string[]{"y","m","y_raised","m_long"},
		new string[]{"y","n","y_raised","n_long"},
		new string[]{"y","o","y_raised","o"},
		new string[]{"y","h","y_raised","h"},
		new string[]{"y","s","y_raised","s"},
		new string[]{"y","u","y_raised","u"},
		new string[]{"y","v","y_raised","v"},
		new string[]{"y","x","y_raised","x"},
		new string[]{"y","z","y_raised","z"},
		new string[]{"y","q","y","q_high"},
		new string[]{"z","g","z","g_shortupper"},
		new string[]{"t","ing","t","ing_short"},
		new string[]{"r","ing","r_long","ing_short"},
		new string[]{"i","ing","i","ing_short"},
		new string[]{"m","ing","m_long","ing_short"},
		new string[]{"y","ing","y_long","ing"},
	};

	private Dictionary<string,List<SquidgeDat> > squidgeDict;

	private SquidgeDat[] squidgeDats = new SquidgeDat[] {
		//variant chars
		new SquidgeDat("y","ing",-7),
		new SquidgeDat("r","ing",-5),
		new SquidgeDat("t","ing",-5),
		new SquidgeDat("i","ing",-5),
		new SquidgeDat("m","ing",-5),
		new SquidgeDat("z","j",-2),
		new SquidgeDat("z","r",-1),
		new SquidgeDat("x","u",-1),
		new SquidgeDat("x","z",-1),
		new SquidgeDat("x","v",-1),
		new SquidgeDat("x","s",-1),
		new SquidgeDat("x","h",-2),
		new SquidgeDat("x","g",-2),
		new SquidgeDat("x","a",-1),
		new SquidgeDat("v","z",-1),
		new SquidgeDat("j","h",-1),
		new SquidgeDat("v","x",-1),
		new SquidgeDat("v","u",-1),
		new SquidgeDat("v","n",-1),
		new SquidgeDat("v","h",-2),
		new SquidgeDat("u","x",-1),
		new SquidgeDat("u","n",-1),
		new SquidgeDat("u","v",-1),
		new SquidgeDat("t","g",-3),
		new SquidgeDat("t","i",-2),
		new SquidgeDat("t","n",-2),
		new SquidgeDat("s","e",-1),
		new SquidgeDat("r","n",-2),
		new SquidgeDat("r","a",-3),
		new SquidgeDat("r","g",-3),
		new SquidgeDat("p","o",-3),
		new SquidgeDat("p","q",-2),
		new SquidgeDat("p","s",-2),
		new SquidgeDat("p","u",-1),
		new SquidgeDat("q","p",-1),
		new SquidgeDat("q","y",-1),
		new SquidgeDat("p","v",-1),
		new SquidgeDat("p","x",-2),
		new SquidgeDat("p","z",-2),
		new SquidgeDat("p","n",-1),
		new SquidgeDat("p","j",-5),
		new SquidgeDat("p","g",-3),
		new SquidgeDat("p","a",-3),
		new SquidgeDat("o","h",-3),
		new SquidgeDat("n","r",-2),
		new SquidgeDat("n","l",-2),
		new SquidgeDat("m","c",-3),
		new SquidgeDat("m","h",-2),
		new SquidgeDat("m","p",-1),
		new SquidgeDat("m","s",-1),
		new SquidgeDat("m","u",-2),
		new SquidgeDat("m","v",-1),
		new SquidgeDat("m","j",-5),
		new SquidgeDat("m","i",-2),
		new SquidgeDat("l","j",-1),
		new SquidgeDat("l","m",-1),
		new SquidgeDat("j","x",-1),
		new SquidgeDat("j","g",-1),
		new SquidgeDat("j","q",-1),
		new SquidgeDat("j","n",-2),
		new SquidgeDat("j","s",-1),
		new SquidgeDat("h","m",-1),
		new SquidgeDat("e","j",-3),
		new SquidgeDat("e","t",-1),
		new SquidgeDat("c","g",-2),
		new SquidgeDat("c","i",-1),
		new SquidgeDat("c","d",-2),
		new SquidgeDat("c","q",-2),
		new SquidgeDat("i","l",-1),
		new SquidgeDat("i","s",-1),
		new SquidgeDat("c","l",-1),
		new SquidgeDat("c","j",-3),
		new SquidgeDat("c","a",-1),
		new SquidgeDat("c","h",-3),
		new SquidgeDat("c","u",-1),
		new SquidgeDat("c","v",-1),
		new SquidgeDat("c","m",-1),
		new SquidgeDat("c","n",-1),
		new SquidgeDat("c","r",-1),
		new SquidgeDat("c","s",-1),
		new SquidgeDat("c","t",-1),
		new SquidgeDat("c","x",-1),
		new SquidgeDat("c","z",-1),
		new SquidgeDat("e","l",-1),
		new SquidgeDat("m","n",-1),
		new SquidgeDat("n","m",-1),
		new SquidgeDat("m","a",-2),
		new SquidgeDat("e","m",-2),
		new SquidgeDat("e","r",-2),
		new SquidgeDat("m","g",-1),
		new SquidgeDat("n","g",-4),
		new SquidgeDat("r","o",-4),
		new SquidgeDat("n","s",-2),
		new SquidgeDat("u","s",-1),
		new SquidgeDat("v","s",-1),
		new SquidgeDat("o","g",-3),
		new SquidgeDat("o","m",-3),
		new SquidgeDat("m","o",-3),
		new SquidgeDat("o","n",-3),
		new SquidgeDat("n","o",-3),
		new SquidgeDat("u","g",-2),
		new SquidgeDat("v","g",-2),
		new SquidgeDat("t","a",-4),
		new SquidgeDat("i","m",-1),

		//regular
		new SquidgeDat("h","j",-1),
		new SquidgeDat("a","e",-1),
		new SquidgeDat("a","o",-1),
		new SquidgeDat("o","e",-1),
		new SquidgeDat("o","c",-1),
		new SquidgeDat("i","j",-4),
		new SquidgeDat("i","o",-3),
		new SquidgeDat("i","r",-1),

		new SquidgeDat("i","t",-1),
		new SquidgeDat("p","i",-1),

		new SquidgeDat("m","n",-2),
		new SquidgeDat("n","m",-2),
		new SquidgeDat("n","t",-2),
		new SquidgeDat("n","j",-2),
		
		new SquidgeDat("o","j",-3),

		new SquidgeDat("r","s",-3),
		new SquidgeDat("r","h",-3),
		new SquidgeDat("r","u",-2),
		new SquidgeDat("r","v",-2),
		new SquidgeDat("r","x",-2),
		new SquidgeDat("r","z",-1),
		new SquidgeDat("r","i",-1),
        
		new SquidgeDat("t","s",-3),
		new SquidgeDat("t","h",-5),
		new SquidgeDat("t","u",-2),
		new SquidgeDat("t","v",-2),
		new SquidgeDat("t","x",-2),
		new SquidgeDat("t","z",-1),
		new SquidgeDat("t","o",-3),

		new SquidgeDat("b","d",-4),
		new SquidgeDat("p","h",-4),
		
		new SquidgeDat("u","h",-2),
		new SquidgeDat("m","z",-1),
		new SquidgeDat("z","m",-1),
		new SquidgeDat("m","x",-1),
		new SquidgeDat("x","n",-1),
	};

	string[][] wordReplacements = new string[][]{
		new string[]{"if","f"},
		new string[]{"of","f"},
		new string[]{"the","th"},
		new string[]{"and","&"},
		new string[]{"is","s"},
		new string[]{"or","r"},
	};

	string nokerning = ",^*`\"<>'.:;+=_";

	bool NoKerning (char ch){
		return nokerning.IndexOf (ch) >= 0;
	}

    int imgWidth=320;
	int imgHeight=200;
	int lineHeight = 16;

	void SetupMaterial(){
		tex = new Texture2D (imgWidth, imgHeight, TextureFormat.RGB24, false);
		tex.filterMode = FilterMode.Point;
		rawImage.texture = tex;
	}
	
	public Dictionary<string,int[,]> glyphs = new Dictionary<string, int[,]>();
	public Dictionary<string,int[,]> halos = new Dictionary<string, int[,]>();
	public Dictionary<string,int[,]> haloDoubles = new Dictionary<string, int[,]>();

	int[,] imageToArray(Texture2D tex){
		var pixels = tex.GetPixels ();
		var w = tex.width;
		var h = tex.height-6;
		var colCount = 0;
		var foundSomething = true;
        for (var i=1; i<w; i++) {
			if (foundSomething) {
				foundSomething=false;
				for (var j=0;j<h;j++){
					var c = pixels[i+w*j];
					if (c.r>0){
						foundSomething=true;
					}
				}
				colCount++;
			}
		}
		var result = new int[colCount,h];
		for (int i=0; i<colCount; i++) {
			for (int j=0;j<h;j++){
				Color pix = pixels[i+1+j*w];
				if (pix.g>0){
					result[i,j]=1;
				} else if (pix.r>0){
					result[i,j]=2;
				}
			}
		}
		return result;
	}
	
	int[,] genHalo(int[,] sprite, bool doub){
		var halo = new int[sprite.GetLength (0)+2, sprite.GetLength (1)+2];
		for (var i=0; i<sprite.GetLength(0); i++) {
			for (var j=0;j<sprite.GetLength(1);j++){
				if (sprite[i,j]==1 || (doub&&sprite[i,j]==2) ){
					halo[i+0,j+0]=1;
					halo[i+1,j+0]=1;
					halo[i+2,j+0]=1;
					halo[i+0,j+1]=1;
					halo[i+1,j+1]=1;
					halo[i+2,j+1]=1;
					halo[i+0,j+2]=1;
					halo[i+1,j+2]=1;
					halo[i+2,j+2]=1;
				}
			}
		}
		return halo;
	}

	bool HaloFits(Color[] screen, int[,] halo, int x, int y){
		var w = halo.GetLength (0);
		var h = halo.GetLength (1);
		for (var i = 0;i<w;i++){
			for (var j = 0;j<h;j++){
				var index = (x-1+i)+imgWidth*(y-1+j);
				if (halo[i,j]>0&&screen[index].r>0.9f){
					return false;
				}
			}
		}
		return true;
	}

	string[][] nameAliases = new string[][] {
		new string[]{"comma",","},
		new string[]{"period","."},
		new string[]{"apostrophe","'"},
		new string[]{"apostrophe","'"},
		new string[]{"dash","-"},
		new string[]{"quote","\""},
		new string[]{"question","?"},
		new string[]{"colon",":"},
		new string[]{"semicolon",";"},
		new string[]{"exclamation","!"},
		new string[]{"at","@"},
		new string[]{"euro","€"},
		new string[]{"pound","£"},
		new string[]{"dollar","$"},
		new string[]{"percent","%"},
		new string[]{"carot","^"},
		new string[]{"ampersand","&"},
		new string[]{"star","*"},
		new string[]{"leftparen","("},
		new string[]{"rightparen",")"},
		new string[]{"underscore","_"},
		new string[]{"equals","="},
		new string[]{"plus","+"},
		new string[]{"leftbracket","["},
		new string[]{"rightbracket","]"},
		new string[]{"rightbrace","{"},
		new string[]{"leftbrace","}"},
		new string[]{"pipe","|"},
		new string[]{"backslash","\\"},
		new string[]{"forwardslash","/"},
		new string[]{"lessthan","<"},
		new string[]{"greaterthan",">"},
		new string[]{"backtick","`"},
		new string[]{"tilde","~"},
		new string[]{"hash","#"},
	};

	void LoadGlyphs(){
		glyphs.Clear ();
		var loaded = Resources.LoadAll ("Letters");
		foreach (var a in loaded) {
			var n = a.name;
			foreach(var na in nameAliases){
				if (na[0]==n){
					n=na[1];
					break;
				}
			}
            Texture2D tex = (Texture2D)a;
			var pixelGrid = imageToArray(tex);
			var halo = genHalo(pixelGrid,false);
			var haloDouble = genHalo(pixelGrid,true);
			glyphs.Add(n,pixelGrid);
			halos.Add(n,halo);
			haloDoubles.Add(n,haloDouble);
		}
	}

	Dictionary<char,string> charDict = new Dictionary<char, string> ();
	List<Character> processStringResult = new  List<Character> ();
	List<Character> ProcessString(string s){
		processStringResult.Clear ();
		for (var i=0;i<s.Length;i++){
			var c = s[i];
			if (i>0){
				var prev_c = processStringResult.Last();
				if (char.IsLetter(c) && prev_c.m==1 && prev_c.c.Length==1 && prev_c.c[0] == c){
					prev_c.m++;
					processStringResult[processStringResult.Count-1]=prev_c;
					continue;
				}
			}
			if (i+2<s.Length && s[i]=='i'&&s[i+1]=='n'&&s[i+2]=='g'){
				processStringResult.Add(new Character("ing"));
				i+=2;
				continue;
			}
			//need to be at start or after line break
			if (i==0 || !char.IsLetter(s[i-1])){
				bool found=false;
				for (var k=0;k<wordReplacements.Length;k++){
					string[] repl = wordReplacements[k];
					var l = repl[0].Length;
					if (l+i>s.Length){
						continue;
					}
					if (l+i==s.Length || !char.IsLetter(s[i+l])){
						bool checksOut=true;
						for (var j=0;j<l;j++){
							if (s[i+j]!=repl[0][j]){
								checksOut=false;
								break;
							}
						}
						if (checksOut){
							var r = repl[1];
							for (var i2=0;i2<r.Length;i2++){
								char c_repl=r[i2];
								if (!charDict.ContainsKey(c_repl)){
									charDict[c_repl]=c_repl.ToString();
								}
								processStringResult.Add(new Character(charDict[c_repl]));
							}
							i+=l-1;
							found=true;
							break;
						}
					}
				}
				if (found){ 
					continue; 
				}

			}
			if (!charDict.ContainsKey(c)){
				charDict[c]=c.ToString();
			}
			processStringResult.Add(new Character(charDict[c]));
		}
		return processStringResult;
	}
	
	int[,] GetGlyph(List<Character> str, int index){
		var ch = str [index];
		if (glyphs.ContainsKey(ch.variant)){
			return glyphs [ch.variant];
		} else {
			print("didn't find " + ch.variant);
			return null;
		}
	}
	int[,] GetHalo(List<Character> str, int index){
		var ch = str [index];
		return ch.m==1? halos [ch.variant]:haloDoubles [ch.variant];
	}

	int Squidge(Character a, Character b){
		if (squidgeDict.ContainsKey (a.c)) {
			var sd_enum = squidgeDict[a.c].GetEnumerator();
			while (sd_enum.MoveNext()){
				var sd = (SquidgeDat)sd_enum.Current;
				if (sd.a==a.c && sd.b==b.c){
					return sd.dx;
				}
			}
		}
		return 0;
	}
	
	Color[] pixels;

	void DrawString(List<Character> str){
		var l = pixels.Length;
		for (int i=0; i<l; i++) {
			pixels[i] = new Color();
		}
		int px = 5;
		int py = imgHeight-lineHeight-6;

		bool justspace = true;
		for (int i=0; i<str.Count; i++) {
			var ch=str[i];
			if (ch.c==" "){
				px+=5;
				justspace=true;
				continue;
			} else if (ch.c=="\t"){
				px+=15;
				justspace=true;
				continue;
			} else if (ch.c=="\n"){
				px=5;
				py-=lineHeight;
				justspace=true;
				continue;
			} 
			var glyph = GetGlyph(str,i);
			if (glyph==null){
				continue;
			}
			var halo = GetHalo(str,i);
			if (i>0&&!justspace){
				var prevCh=str[i-1];
				var squidgeOffset = Squidge(prevCh,ch);
				if (squidgeOffset<0){
					px+=squidgeOffset-1;
				} else {
					var any=false;
					if ((NoKerning(str[i].c[0]))||(i>0&&NoKerning(str[i-1].c[0])&&str[i-1].c!="'")){
						px--;
					} else {
						var dx=0;
						while (HaloFits(pixels,halo,px+dx,py)){
							any=true;
							dx--;
							if (px+dx<=0){
								px=1;
								dx=0;
								break;
							}
						}
						if (any){
							px=px+dx+1;
							if (str[i].c=="d2"){
								if (prevCh.c=="o"){
									px-=2;
								} else if ("ijz".Contains(prevCh.c)){
									px-=2;
								} else if ("ehmnrst".Contains(prevCh.c)){
									px-=3;
								}  else if ("l".Contains(prevCh.c)){
									px-=2;
									var t = str[i];
									t.variant="d_short";
									str[i]=t;
									glyph = GetGlyph(str,i);
									halo = GetHalo(str,i);
								} else {
									px-=4;
								}
							}
						}
					}
				}
			}
			justspace=false;
			/*
			for (int x=0;x<halo.GetLength(0);x++){
				for (int y=0;y<halo.GetLength(1);y++){
					var col = Color.black;
					var index = halo[x,y];
					if ((index==1) || 
					    (index==2&&ch.m>1)){
//						pixels[px+x-1+(py+y-1)*imgWidth]=Color.gray;
					}
				}
			}*/
			
			var w= glyph.GetLength(0);
			var h = glyph.GetLength(1);

			if (px+w>imgWidth){
				px=5;
				py-=lineHeight;
			}
			
			if (py<=0){
				inputString=inputString.Substring(0,inputString.Length-1);
				break;
			}

			for (int x=0;x<w;x++){
				for (int y=0;y<h;y++){
					var index = glyph[x,y];
					if ((index==1) || 
						(index==2&&ch.m>1)){
						int index2 = px+x+(py+y)*imgWidth;
						pixels[index2]=Color.white;
					}
				}
			}
			px+=w+1;
		}

		tex.SetPixels (pixels);
		tex.Apply ();
	}

	void replaceCharacters(List<Character> processed){
		for (var i=1; i<processed.Count; i++) {
			var a = processed[i-1];
			var b = processed[i];
			if (variantDict.ContainsKey(a.c)){
				var dictenum = variantDict[a.c].GetEnumerator();
				while (dictenum.MoveNext()){
					var variant =  dictenum.Current;
					if (variant[0]==a.c && variant[1]==b.c){
						if (variant[0]!=variant[2]){
							a.variant=variant[2];
						}
						processed[i-1]=a;
						if (variant[1]!=variant[3]){
							b.variant=variant[3];
						}
						processed[i] = b;
					}
				}
			}
		}
		for (var i=2;i<processed.Count;i++){
			var b = processed[i-2];
			var c = processed[i-1];
			var d = processed[i];
			if (b.variant =="b" && d.variant=="d" && c.noLow()){
				d.variant="d_long";
				d.c = "d2";
				processed[i]=d;
			}
		}
	}

	void OnApplicationQuit() {
		SaveString();
	}

	// Use this for initialization
	void Start () {
		
		pagex = PlayerPrefs.GetInt("pagex",0);
		LoadString ();
		pixels = new Color[imgWidth*imgHeight];	
		squidgeDict = new Dictionary<string, List<SquidgeDat> > ();
		foreach (var sd in squidgeDats) {
			if (squidgeDict.ContainsKey(sd.a)==false){
				squidgeDict[sd.a]= new List<SquidgeDat>();
			}
			squidgeDict[sd.a].Add (sd);
		}
		variantDict = new Dictionary<string, List<string[]>> ();
		foreach (var v in variantselect) {
			if (variantDict.ContainsKey(v[0])==false){
				variantDict[v[0]] = new List<string[]>();
			}
			variantDict[v[0]].Add(v);
		}
		SetupMaterial ();
		LoadGlyphs ();
		oldString = inputString;
		var processed = ProcessString (inputString);
		processed.Add ( new Character("_"));
		replaceCharacters (processed);
		DrawString (processed);
    }
    
	string oldString = "";
	private int pagex=0;

	void LoadString(){
		inputString = "";
		var path = Application.persistentDataPath+"/f"+pagex + ".txt";
		if (System.IO.File.Exists (path)) {
			inputString = System.IO.File.ReadAllText (path);
		}
	}

	void SaveString(){			
		var path = Application.persistentDataPath+"/f"+pagex  + ".txt";
		System.IO.File.WriteAllText (path,inputString);
	}

	void MovePage(int newx){	
		if (newx < 0 || newx > 9) {
			return;
		}
		pagex = PlayerPrefs.GetInt("pagex",0);
		SaveString ();
		pagex = newx;
		LoadString ();
		PlayerPrefs.SetInt ("pagex", pagex);
	}
	
	private TextEditor te = new TextEditor ();
	string allowedChars = " \t\nw";
	bool apple=false;
	bool shift=false;

	void DoCopy(bool copyall){
		if (copyall){
			var str = "";
			var oldpage = pagex;
			for (int i=0;i<10;i++){
				if (i>0){
					str+="\n\n-----\n\n";
				}
				MovePage(i);
				str+=inputString;
			}
			MovePage (oldpage);
			te.content= new GUIContent(str);
		} else {
			te.content = new GUIContent(inputString);
		}
		te.SelectAll();
		te.Copy();
	}

	void SaveAll(){
		var str = "";
		var oldpage = pagex;
		for (int i=0;i<10;i++){
			if (i>0){
				str+="\n\n-----\n\n";
			}
			MovePage(i);
			str+=inputString;
		}
		MovePage (oldpage);
		
		var path = Application.persistentDataPath+"/all.txt";
		System.IO.File.WriteAllText (path,str);
	}

	void OpenDir(){
		SaveAll ();
		var p = new System.Diagnostics.Process();
		p.StartInfo.RedirectStandardError=true;
		p.StartInfo.RedirectStandardOutput=true;
		p.StartInfo.UseShellExecute=false;
		p.StartInfo.FileName="/usr/bin/open";
		p.StartInfo.Arguments = '"'+Application.persistentDataPath+'"';
		p.Start();
		inputString+=(p.StandardOutput.ReadToEnd());
		inputString+=(p.StandardError.ReadToEnd());
	}

	void Clear(bool all){
		if (all) {
			DoCopy (true);
			for (int i=0; i<10; i++) {
				inputString = "";
				MovePage (i);
				inputString = "";
			}
			MovePage (0);
		} else {
			inputString="";
		}
	}

	int[] pageIndexes = {9,0,1,2,3,4,5,6,7,8};
	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
		if (Input.GetKeyDown (KeyCode.LeftCommand)) {
			apple=true;
		} 
		if (Input.GetKeyDown (KeyCode.F1)) {
			OpenDir();
		}
		if (Input.GetKeyUp (KeyCode.LeftCommand)) {
			apple=false;
		} 
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			shift=true;
		} 
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			shift=false;
		} 
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Clear (shift);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			MovePage(pagex-1);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			MovePage(pagex+1);
		}
		/*
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			MovePage(-1,0);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			MovePage(1,0);
		}*/
		var s = Input.inputString;
		for (var si=0;si<s.Length;si++){

			var c = char.ToLower(s[si]);
			if (c == '\b') {
				if (inputString.Length > 0) {
					if (apple && shift){		
						Clear(true);
					} else if (apple){
						inputString = "";
					} else {
						inputString = inputString.Substring (0, inputString.Length - 1);
					}
				}
			} else if (glyphs.ContainsKey(c.ToString())||allowedChars.IndexOf(c)>=0) {
				if (apple && c=='s'){
					SaveString();
				} else if (apple && char.IsDigit(c)){
					Debug.Log("moving page to "+c);
					MovePage(pageIndexes[int.Parse(c.ToString())]);
				} else if (apple && c=='c'){
					DoCopy(shift);
				} else if (apple && c=='v'){
					te.content = new GUIContent("");
					te.Paste();
					print (te.content.text);
					s += te.content.text;
					apple=false;
				} else if (apple && c=='o'){
					OpenDir();
				} else {
					inputString += c;
				}
			}
		}
		if (oldString != inputString) {
			oldString=inputString;
			
			var processed = ProcessString (inputString);
			processed.Add ( new Character("_"));
			replaceCharacters (processed);
			DrawString (processed);
		}
	
	}
}
