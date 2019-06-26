using System.Collections;
using System.Collections.Generic;
using System.Text;
//using UnityEngine;

namespace JLG.gift.cSharp.formating {
	public class TextFormatter {

		public static string translateCodes(string text) {
			char[] t = text.Trim().ToCharArray();
			//string newTest = "";
			StringBuilder sb = new StringBuilder();

			Stack<object> ends = new Stack<object>();

			for (int x = 0; x < t.Length; x++) {
				if (t[x] == ColorCode.COLORSYMBOL) {

					x++;

					if (x >= t.Length) {
						//newTest = newTest + t[x];
						break;
					}
					ColorCode code = ColorCode.getCodeFromSymbol(t[x]);

					if (code is null) {
						//newTest = newTest + ColorCode.COLORSYMBOL + t[x];
						sb.Append(ColorCode.COLORSYMBOL);
						sb.Append(t[x]);
						continue;
					} else if (code == ColorCode.IGNORE) {
						//newTest = newTest + ColorCode.COLORSYMBOL;
						sb.Append(ColorCode.COLORSYMBOL);
					} else if (code == ColorCode.CUSTOM) {

						while (ends.Count > 0) {
							object popped = ends.Pop();
							if (popped is FormatCode) {
								FormatCode p = (FormatCode)popped;
								//newTest = newTest + p.endTag;
								sb.Append(p.endTag);
							} else {
								//newTest = newTest + "</color>";
								sb.Append("</color>");
							}
						}

						string hex = "";

						for (int y = 0; y < 6; y++) {
							hex = hex + t[x + y];
							x++;
						}

						//newTest = newTest + "<color=#" + hex + ">";
						sb.Append("<color=#");
						sb.Append(hex);
						sb.Append(">");

						ends.Push(code);
					} else {
						while (ends.Count > 0) {
							object popped = ends.Pop();
							if (popped is FormatCode) {
								FormatCode p = (FormatCode)popped;
								//newTest = newTest + p.endTag;
								sb.Append(p.endTag);
							} else {
								//newTest = newTest + "</color>";
								sb.Append("</color>");
							}
						}
						//newTest = newTest + "<color=#" + code.hex + ">";
						sb.Append("<color=#");
						sb.Append(code.hex);
						sb.Append(">");

						ends.Push(code);
					}

				} else if (t[x] == FormatCode.FORMATSYMBOL) {

					x++;

					if (x >= t.Length) {
						//newTest = newTest + t[x];
						break;
					}
					FormatCode code = FormatCode.getCodeFromSymbol(t[x]);

					if (code is null) {
						//newTest = newTest + FormatCode.FORMATSYMBOL + t[x];
						sb.Append(FormatCode.FORMATSYMBOL);
						sb.Append(t[x]);
						continue;
					} else if (code == FormatCode.IGNORE) {
						//newTest = newTest + FormatCode.FORMATSYMBOL;
						sb.Append(FormatCode.FORMATSYMBOL);
					} else if (code == FormatCode.RESETALL) {
						while (ends.Count > 0) {
							object popped = ends.Pop();
							if (popped is FormatCode) {
								FormatCode p = (FormatCode)popped;
								//newTest = newTest + p.endTag;
								sb.Append(p.endTag);
							} else {
								//newTest = newTest + "</color>";
								sb.Append("</color>");
							}
						}
					} else {
						//newTest = newTest + code.tag;
						sb.Append(code.tag);

						ends.Push(code);
					}

				} else {
					//newTest = newTest + t[x];
					sb.Append(t[x]);
				}
			}

			while (ends.Count > 0) {
				object popped = ends.Pop();
				if (popped is FormatCode) {
					FormatCode p = (FormatCode)popped;
					//newTest = newTest + p.endTag;
					sb.Append(p.endTag);
				} else {
					//newTest = newTest + "</color>";
					sb.Append("</color>");
				}
			}

			//return newTest;
			return sb.ToString();

		}

		public static string getRaw(string text) {
			char[] t = text.Trim().ToCharArray();
			StringBuilder sb = new StringBuilder();

			for (int x = 0; x < t.Length; x++) {
				if (t[x] == ColorCode.COLORSYMBOL) {
					x++;
					if (x >= t.Length) {
						break;
					}
					if (ColorCode.getCodeFromSymbol(t[x]) is null) {
						sb.Append(t[x - 1]);
						sb.Append(t[x]);
					}
				} else if (t[x] == FormatCode.FORMATSYMBOL) {
					x++;
					if (x >= t.Length) {
						break;
					}
					if (FormatCode.getCodeFromSymbol(t[x]) is null) {
						sb.Append(t[x - 1]);
						sb.Append(t[x]);
					}
				} else {
					sb.Append(t[x]);
				}
			}
			return sb.ToString();
		}

		public static string combineDisplayTag(string display, string tag) {
			return display + "\n\n" + tag;
		}

		public class FormatCode {

			public static readonly char FORMATSYMBOL = '&';

			public static readonly FormatCode BOLD = new FormatCode('b', "<b>", "</b>");
			public static readonly FormatCode ITALIC = new FormatCode('i', "<i>", "</i>");
			public static readonly FormatCode IGNORE = new FormatCode('&', null, null);
			public static readonly FormatCode RESETALL = new FormatCode('r', null, null);

			private static readonly FormatCode[] codes = {
				BOLD, ITALIC, IGNORE, RESETALL
			};

			public static FormatCode getCodeFromSymbol(char sym) {
				foreach (FormatCode check in codes) {
					if (check.code == sym)
						return check;
				}
				return null;
			}

			public char code {
				get; private set;
			}

			public string tag {
				get; private set;
			}

			public string endTag {
				get; private set;
			}

			public FormatCode(char code, string tag, string e) {
				//(this.code, this.tag, endTag) = (code, tag, e);
				this.code = code;
				this.tag = tag;
				endTag = e;
			}
		}

		public class ColorCode {

			public static readonly char COLORSYMBOL = '$';

			public static readonly ColorCode BLACK = new ColorCode('0', "000000");
			public static readonly ColorCode DARK_BLUE = new ColorCode('1', "0000AA");
			public static readonly ColorCode DARK_GREEN = new ColorCode('2', "00AA00");
			public static readonly ColorCode DARK_AQUA = new ColorCode('3', "00AAAA");
			public static readonly ColorCode DARK_RED = new ColorCode('4', "AA0000");
			public static readonly ColorCode DARK_PURPLE = new ColorCode('5', "AA00AA");
			public static readonly ColorCode GOLD = new ColorCode('6', "FFAA00");
			public static readonly ColorCode GRAY = new ColorCode('7', "AAAAAA");
			public static readonly ColorCode DARK_GRAY = new ColorCode('8', "555555");
			public static readonly ColorCode BLUE = new ColorCode('9', "5555FF");
			public static readonly ColorCode GREEN = new ColorCode('a', "55FF55");
			public static readonly ColorCode AQUA = new ColorCode('b', "55FFFF");
			public static readonly ColorCode RED = new ColorCode('c', "FF5555");
			public static readonly ColorCode LIGHT_PURPLE = new ColorCode('d', "FF55FF");
			public static readonly ColorCode YELLOW = new ColorCode('e', "FFFF55");
			public static readonly ColorCode WHITE = new ColorCode('f', "FFFFFF");

			public static readonly ColorCode IGNORE = new ColorCode('$', null);
			public static readonly ColorCode CUSTOM = new ColorCode('*', null);
			//public static readonly ColorCode RESET = new ColorCode('r', null);

			private static readonly ColorCode[] codes = {
				BLACK, DARK_BLUE, DARK_GREEN, DARK_AQUA, DARK_RED, DARK_PURPLE,GOLD,GRAY,DARK_GRAY,BLUE,GREEN,AQUA,RED,LIGHT_PURPLE,YELLOW,WHITE, IGNORE, CUSTOM
			};

			public static ColorCode getCodeFromSymbol(char sym) {
				foreach (ColorCode check in codes) {
					if (check.code == sym)
						return check;
				}
				return null;
			}

			public char code {
				get; private set;
			}

			public string hex {
				get; private set;
			}

			public ColorCode(char code, string hex) {
				//(this.code, this.hex) = (code, hex);
				this.code = code;
				this.hex = hex;
			}

			public override string ToString() {
				//return base.ToString();
				return "$" + code;
			}

		}



	}
}