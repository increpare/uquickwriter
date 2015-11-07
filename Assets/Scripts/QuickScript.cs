using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class QuickScript : MonoBehaviour {
	[TextArea(3,10)]
	public string inputString="";

	public Texture2D tex;
	public UnityEngine.UI.RawImage rawImage;

	public class Character {
		public char c; //character
		public int m; //multiplicity
		public string variant;
		public Character(char c){
			if (c=='w'){
				this.c='u';
				this.m=2;
			} else {
				this.c=c;
				this.m=1;
			}
			this.variant=this.c.ToString();

		}

		public bool subO(){
			return "aceo".IndexOf(c)>=0;
		}
		
		public bool noLow(){
			return "acehijklmnorstuvz".IndexOf (c) >= 0;
		}
		
		public bool noLowLeft(){
			return "acehijklmnoqrstuvz".IndexOf (c) >= 0;
		}

		public bool noLowRight(){
			return "acehijklmnoprstuvyz".IndexOf (c) >= 0;
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

	private string[][] variantselect = new string[][]{
		new string[]{"cd","c_flatlow","d_raised"},
		new string[]{"cl","c_flatlonglow","l"},
		new string[]{"cj","c_flatlonglow","j"},
		new string[]{"ca","c_flatlonghigh","a_flat"},
		new string[]{"ch","c_flatlonghigh","h"},
		new string[]{"cu","c_flatlonghigh","u"},
		new string[]{"cv","c_flathigh","v"},
		new string[]{"cm","c_flatlow","m_long"},
		new string[]{"cn","c_flathigh","n_long"},
		new string[]{"el","e_longlow","l"},
		new string[]{"mn","m_long","n_long"},
		new string[]{"nm","n_long","m_long"},
		new string[]{"nt","n_long","t"},
		new string[]{"nj","n_long","j"},
		new string[]{"ma","m_long","a_flat"},
		new string[]{"em","e","m_long"},
		new string[]{"er","e_longlow","r"},
		new string[]{"mg","m_long","g_flatupper"},
		new string[]{"ng","n_long","g_flatupper"},
		new string[]{"ro","r_long","o"},
		new string[]{"ns","n_long","s"},
		new string[]{"us","u","s_shortlow"},
		new string[]{"vs","v","s_shortlow"},
		new string[]{"ag","a","g_shortflatupper"},
		new string[]{"eg","e","g_shortflatupper"},
		new string[]{"og","o","g_flatupper"},
		new string[]{"om","o","m_long"},
		new string[]{"mo","m_long","o"},
		new string[]{"on","o","n_long"},
		new string[]{"no","n_long","o"},
		new string[]{"ug","u","g_shortflatupper"},
		new string[]{"vg","v","g_shortflatupper"},
		new string[]{"ta","t","a_flat"},
		new string[]{"in","i","n_long"},
		new string[]{"mz","m_long","z"},
		new string[]{"zm","z","m_long"},
		new string[]{"mx","m_long","x"},
		new string[]{"xn","x","n_long"},
		new string[]{"im","i","m_long"},
		new string[]{"aq","a","q_high"},
		new string[]{"bf","b_raised","f_lowered"},
		new string[]{"bg","b_raised","g_longlow"},
		new string[]{"bx","b","x_raised"},
		new string[]{"cg","c_flathigh","g_flatupper"},
		new string[]{"ci","c_flatlonghigh","i"},
		new string[]{"cq","c_flathigh","q"},
		new string[]{"cr","c_flatlow","r"},
		new string[]{"cs","c_flatlonghigh","s_shortlow"},
		new string[]{"ct","c_flatlonglow","t"},
		new string[]{"cx","c_flatlonghigh","x"},
		new string[]{"cz","c_flatlonghigh","z"},
		new string[]{"ej","e_longlow","j"},
		new string[]{"et","e_longlow","t"},
		new string[]{"hg","h","g_shortupper"},
		new string[]{"hm","h","m_long"},
		new string[]{"hq","h","q_high"},
		new string[]{"jg","j","g_shortupper"},
		new string[]{"lg","l","g_shortupper"},
		new string[]{"lm","l","m_long"},
		new string[]{"jn","j","n_long"},
		new string[]{"js","j","s_shortlow"},
		new string[]{"jx","j","x_wide"},
		new string[]{"mc","m_long","c"},
		new string[]{"me","m_long","e"},
		new string[]{"mh","m_long","h"},
		new string[]{"mi","m_long","i"},
		new string[]{"mp","m_long","p"},
		new string[]{"ms","m_long","s"},
		new string[]{"mu","m_long","u"},
		new string[]{"mv","m_long","v"},
		new string[]{"mj","m_long","j"},
		new string[]{"nl","n_long","l"},
		new string[]{"nr","n_long","r"},
		new string[]{"pa","p_long","a_flat"},
		new string[]{"pg","p_long","g_shortupper"},
		new string[]{"pj","p_long","j"},
		new string[]{"pn","p","n_long"},
		new string[]{"po","p_long","o"},
		new string[]{"ra","r","a_flat"},
		new string[]{"rq","r","q_high"},
		new string[]{"mq","m","q_high"},
		new string[]{"rg","r","g_shortupper"},
		new string[]{"rn","r","n_long"},
		new string[]{"sg","s","g_shortupper"},
		new string[]{"sq","s","q"},
		new string[]{"tg","t","g_shortupper"},
		new string[]{"tn","t","n_long"},
		new string[]{"tq","t","q_high"},
		new string[]{"uq","u","q_high"},
		new string[]{"un","u","n_long"},
		new string[]{"ux","u","x_wide"},
		new string[]{"vn","v","n_long"},
		new string[]{"vq","v","q_high"},
		new string[]{"xa","x","a_flatlong"},
		new string[]{"xd","x_raised","d"},
		new string[]{"xg","x_wide","g_flatupper"},
		new string[]{"xq","x","q_high"},
		new string[]{"xs","x","s_shortlow"},
		new string[]{"xz","x_wide","z"},
		new string[]{"xu","x_wide","u"},
		new string[]{"xu","x_wide","u"},
		new string[]{"ya","y_raised","a"},
		new string[]{"yc","y_raised","c"},
		new string[]{"ye","y_raised","e"},
		new string[]{"yg","y_raised","g"},
		new string[]{"yi","y_raised","i"},
		new string[]{"ym","y_raised","m_long"},
		new string[]{"yn","y_raised","n_long"},
		new string[]{"yo","y_raised","o"},
		new string[]{"yh","y_raised","h"},
		new string[]{"ys","y_raised","s"},
		new string[]{"yu","y_raised","u"},
		new string[]{"yv","y_raised","v"},
		new string[]{"yx","y_raised","x"},
		new string[]{"yz","y_raised","z"},
		new string[]{"yq","y","q_high"},
		new string[]{"zg","z","g_shortupper"},
	};

	private SquidgeDat[] squidgeDats = new SquidgeDat[] {
		//variant chars
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
		new SquidgeDat("v","x",-1),
		new SquidgeDat("v","u",-1),
		new SquidgeDat("v","n",-1),
		new SquidgeDat("v","h",-2),
		new SquidgeDat("v","i",-1),
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
		new SquidgeDat("p","g",-1),
		new SquidgeDat("p","a",-3),
		new SquidgeDat("o","h",-3),
		new SquidgeDat("n","r",-2),
		new SquidgeDat("n","l",-2),
		new SquidgeDat("m","c",-3),
		new SquidgeDat("m","e",-3),
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

    int imgWidth=128;
	int imgHeight=128;
	int lineHeight = 17;

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
		var h = tex.height;
		var colCount = 0;
		var foundSomething = true;
        for (var i=1; i<w; i++) {
			if (foundSomething) {
				foundSomething=false;
				for (var j=0;j<h;j++){
					var c = pixels[i+w*j];
					if (!c.Equals(Color.black)){
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
		for (var i = 0;i<halo.GetLength(0);i++){
			for (var j = 0;j<halo.GetLength(1);j++){
				var index = (x-1+i)+imgWidth*(y-1+j);
				if (halo[i,j]>0&&screen[index].r>0.9f){
					return false;
				}
			}
		}
		return true;
	}

	void LoadGlyphs(){
		glyphs.Clear ();
		var loaded = Resources.LoadAll ("Letters");
		foreach (var a in loaded) {
            Texture2D tex = (Texture2D)a;
			var pixelGrid = imageToArray(tex);
			var halo = genHalo(pixelGrid,false);
			var haloDouble = genHalo(pixelGrid,true);
			glyphs.Add(a.name,pixelGrid);
			halos.Add(a.name,halo);
			haloDoubles.Add(a.name,haloDouble);
		}
	}

	List<Character> ProcessString(string s){
		List<Character> result = new List<Character>();
		for (var i=0;i<s.Length;i++){
			var c = s[i];
			if (i>0){
				var prev_c = result.Last();
				if (!char.IsWhiteSpace(c) && prev_c.m==1 && prev_c.c == c){
					prev_c.m++;
					continue;
				}
			}
			result.Add(new Character(c));
		}
		return result;
	}
	
	int[,] GetGlyph(List<Character> str, int index){
		var ch = str [index];
		print (ch.variant);
		return glyphs [ch.variant];
	}
	int[,] GetHalo(List<Character> str, int index){
		var ch = str [index];
		return ch.m==1? halos [ch.variant]:haloDoubles [ch.variant];
	}


	int Squidge(Character a, Character b){
		foreach (var sd in squidgeDats) {
			if (sd.a==a.c.ToString() && sd.b==b.c.ToString()){
				return sd.dx;
			}
		}
		return 0;
	}
	void DrawString(List<Character> str){
		Color[] pixels = new Color[imgWidth*imgHeight];
		int px = 1;
		int py = imgHeight-lineHeight-6;

		bool justspace = true;
		for (int i=0; i<str.Count; i++) {
			var ch=str[i];
			print ("printing " + ch.c);
			if (ch.c==' '){
				px+=5;
				justspace=true;
				continue;
			} else if (ch.c=='\n'){
				px=1;
				py-=lineHeight;
				justspace=true;
				continue;
			} 
			var glyph = GetGlyph(str,i);
			var halo = GetHalo(str,i);
			if (i>0&&!justspace){
				var prevCh=str[i-1];
				var squidgeOffset = Squidge(prevCh,ch);
				if (squidgeOffset<0){
					px+=squidgeOffset-1;
				} else {
					var dx=0;
					var any=false;
					while (HaloFits(pixels,halo,px+dx,py)){
						any=true;
						print (ch.variant+" fits " + dx);
						dx--;
						if (px+dx<=0){
							px=1;
							dx=0;
							break;
						}
					}
					if (any){
						px=px+dx+1;
					}
				}
			}
			justspace=false;
			for (int x=0;x<halo.GetLength(0);x++){
				for (int y=0;y<halo.GetLength(1);y++){
					var col = Color.black;
					var index = halo[x,y];
					if ((index==1) || 
					    (index==2&&ch.m>1)){
//						pixels[px+x-1+(py+y-1)*imgWidth]=Color.gray;
					}
				}
			}

			for (int x=0;x<glyph.GetLength(0);x++){
				for (int y=0;y<glyph.GetLength(1);y++){
					var col = Color.black;
					var index = glyph[x,y];
					if ((index==1) || 
						(index==2&&ch.m>1)){
						int index2 = px+x+(py+y)*imgWidth;
						pixels[index2]=Color.white;
					}
				}
			}
			px+=glyph.GetLength(0)+1;
		}

		tex.SetPixels (pixels);
		tex.Apply ();
	}

	void replaceCharacters(List<Character> processed){
		for (var i=1; i<processed.Count; i++) {
			var a = processed[i-1];
			var b = processed[i];
			foreach (var variant in variantselect){
				if (variant[0][0]==a.c && variant[0][1]==b.c){
					a.variant=variant[1];
					b.variant=variant[2];
				}
			}

		}
	}

	// Use this for initialization
	void Start () {
		var str = inputString.ToLower ();
		SetupMaterial ();
		LoadGlyphs ();
		var processed = ProcessString (str);
		replaceCharacters (processed);
		DrawString (processed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
