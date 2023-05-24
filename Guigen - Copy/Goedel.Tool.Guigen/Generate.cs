// Script Syntax Version:  1.0

//  © 2015-2021 by Threshold Secrets LLC.
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  
//  
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Guigen;
public partial class Generate : global::Goedel.Registry.Script {

	

	//
	// GeneratePs1
	//
	public void GeneratePs1 (Guigen Guigen) {
		 Guigen._InitChildren();
		foreach  (_Choice Item in Guigen.Top) {
			switch (Item._Tag ()) {
				case GuigenType.Application: {
				  Application application = (Application) Item; 
				_Output.Write ("\n{0}", _Indent);
				foreach  (var tagValue in application.DictionaryIconsByFile) {
					 var tag = tagValue.Key;
					_Output.Write ("$source = $args[0]\n{0}", _Indent);
					_Output.Write ("$target = $args[1]\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("copy $source\\{1}  $target\\{2}\n{0}", _Indent, tag, tag);
					}
			break; }
				}
			}
		_Output.Write ("\n{0}", _Indent);
		}
	

	//
	// GenerateCS
	//
	public void GenerateCS (Guigen Guigen) {
		 Guigen._InitChildren();
		_Output.Write ("#region // Copyright \n{0}", _Indent);
		 // Boilerplate.Header (_Output, "//  ", GenerateTime);
		foreach  (var Item in Guigen.Top) {
			switch (Item._Tag ()) {
				case GuigenType.Copyright: {
				  Copyright Copyright = (Copyright) Item; 
				switch (Copyright.License._Tag ()) {
					case GuigenType.MITLicense: { 
					
					 Boilerplate.MITLicense (_Output, "//  ", "Copyright (c) " + Copyright.Date, Copyright.Holder);
					break; }
					case GuigenType.BSD2License: { 
					
					 Boilerplate.BSD2License (_Output, "//  ", "Copyright (c) " +  Copyright.Date, Copyright.Holder);
					break; }
					case GuigenType.BSD3License: { 
					
					 Boilerplate.BSD3License (_Output, "//  ", "Copyright (c) " +  Copyright.Date, Copyright.Holder);
					break; }
					case GuigenType.ISCLicense: { 
					
					 Boilerplate.ISCLicense (_Output, "//  ", "Copyright (c) " +  Copyright.Date, Copyright.Holder);
					break; }
					case GuigenType.Apache2License: { 
					
					 Boilerplate.Apache2License (_Output, "//  ", "Copyright (c) " +  Copyright.Date, Copyright.Holder);
					break; }
					case GuigenType.OtherLicense: {
					  OtherLicense License = (OtherLicense) Copyright.License; 
					_Output.Write ("// Copyright (1) {1} by {2}\n{0}", _Indent, Copyright.Date, Copyright.Holder);
					_Output.Write ("// {1}\n{0}", _Indent, License.Text);
				break; }
					}
			break; }
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#pragma warning disable IDE0161 // Convert to file-scoped namespace\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (_Choice Item in Guigen.Top) {
			switch (Item._Tag ()) {
				case GuigenType.Application: {
				  Application application = (Application) Item; 
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("namespace {1} {{\n{0}", _Indent, application.Namespace);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	///<summary></summary> \n{0}", _Indent);
				_Output.Write ("	public partial class {1} : GuiApplication {{\n{0}", _Indent, application.Id);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				foreach  (var tagValue in application.DictionaryMenuEntries) {
					 var tag = tagValue.Key;
					 var menuEntry = tagValue.Value;
					_Output.Write ("		// Menu Item {1}\n{0}", _Indent, tag);
					_Output.Write ("	public GuiMenuEntry Action_{1} = new (\"{2}\", \"{3}\", \"{4}\", \"{5}\" );\n{0}", _Indent, tag, menuEntry.Id, menuEntry.Prompt, menuEntry.Icon?.File, menuEntry.Text?.Prompt);
					}
				_Output.Write ("\n{0}", _Indent);
				foreach  (var tagValue in application.DictionaryIcons) {
					 var tag = tagValue.Key;
					 var icon = tagValue.Value;
					_Output.Write ("	public GuiIcon Icon_{1} = new (\"{2}\", \"{3}\");\n{0}", _Indent, tag, icon.Id, icon.File);
					_Output.Write ("		// Icon Entry {1}\n{0}", _Indent, tag);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("		///<summary>Environment declarations</summary> \n{0}", _Indent);
				_Output.Write ("		public static List <GuiEnvironment> Environments = new () {{", _Indent);
				
				 var separator = new Separator (",");
				foreach  (var environment in application.Environments) {
					_Output.Write ("{1}\n{0}", _Indent, separator);
					_Output.Write ("			new (\"{1}\", \"{2}\") {{\n{0}", _Indent, environment.Id, environment.Icon?.File);
					_Output.Write ("				Menus = new () {{", _Indent);
					 var separator2 = new Separator (",");
					foreach  (var menu in environment.Menus) {
						_Output.Write ("{1}\n{0}", _Indent, separator2);
						_Output.Write ("					new () {{\n{0}", _Indent);
						_Output.Write ("						Entries = new () {{", _Indent);
						 MakeMenuEntries(menu.Entries);
						_Output.Write ("							}}\n{0}", _Indent);
						_Output.Write ("						}}", _Indent);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("					}},\n{0}", _Indent);
					_Output.Write ("				Stores = new () {{", _Indent);
					 separator2.Reset();
					foreach  (var store in environment.Stores) {
						_Output.Write ("{1}\n{0}", _Indent, separator2);
						switch (store._Tag ()) {
							case GuigenType.Catalog: {
							  Catalog catalog = (Catalog) store; 
							_Output.Write ("					new () {{\n{0}", _Indent);
							_Output.Write ("						Mode = GuiTableMode.Catalog,", _Indent);
							
							 MakeStoreEntries(catalog);
							_Output.Write ("						}}", _Indent);
							break; }
							case GuigenType.Spool: {
							  Spool spool = (Spool) store; 
							_Output.Write ("					new () {{\n{0}", _Indent);
							_Output.Write ("						Mode = GuiTableMode.Spool,", _Indent);
							
							 MakeStoreEntries(spool);
							_Output.Write ("						}}", _Indent);
						break; }
							}
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("					}}\n{0}", _Indent);
					_Output.Write ("				}}", _Indent);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("			}};\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("		///<summary>Structure declarations</summary> \n{0}", _Indent);
				_Output.Write ("		public static List <GuiStructure> Structures = new () {{", _Indent);
				
				 separator.Reset();
				foreach  (var structure in application.Structures) {
					_Output.Write ("{1}\n{0}", _Indent, separator);
					_Output.Write ("			new (typeof({1}), \"{2}\")", _Indent, structure.Id, structure.Icon?.File);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("			}};\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("		///<summary>Icon declarations</summary> \n{0}", _Indent);
				_Output.Write ("		public static List <GuiIcon> Icons = new () {{", _Indent);
				
				 separator.Reset();
				foreach  (var icon in application.Icons) {
					_Output.Write ("{1}\n{0}", _Indent, separator);
					_Output.Write ("			new (\"{1}\", \"{2}\")", _Indent, icon.Id, icon.File);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("			}};\n{0}", _Indent);
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("	}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
			break; }
				}
			}
		_Output.Write ("\n{0}", _Indent);
		}
	

	//
	// MakeMenuEntries
	//
	public void MakeMenuEntries (List<MenuEntry> menuEntries) {
		 var separator = new Separator (",");
		foreach  (var menuEntry in menuEntries) {
			_Output.Write ("{1}\n{0}", _Indent, separator);
			_Output.Write ("							new (\"{1}\", \"{2}\", \"{3}\", \"{4}\" )", _Indent, menuEntry.Id, menuEntry.Prompt, menuEntry.Icon?.File, menuEntry.Text?.Prompt);
			}
		}
	

	//
	// MakeMenuEntries2
	//
	public void MakeMenuEntries2 (List<MenuEntry> menuEntries) {
		 var separator = new Separator (",");
		foreach  (var menuEntry in menuEntries) {
			_Output.Write ("{1}\n{0}", _Indent, separator);
			_Output.Write ("									new (\"{1}\", \"{2}\", \"{3}\", \"{4}\" )", _Indent, menuEntry.Id, menuEntry.Prompt, menuEntry.Icon?.File, menuEntry.Text?.Prompt);
			}
		}
	

	//
	// MakeStoreEntries
	//
	public void MakeStoreEntries (IHaveActions store) {
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("						Defaults = new () {{", _Indent);
		 var separator = new Separator (",");
		foreach  (var defaultEntry in store.Defaults) {
			_Output.Write ("{1}\n{0}", _Indent, separator);
			_Output.Write ("							new () {{\n{0}", _Indent);
			_Output.Write ("								Entries = new () {{", _Indent);
			 MakeMenuEntries2(defaultEntry.Entries);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("									}}\n{0}", _Indent);
			_Output.Write ("								}}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("							}},\n{0}", _Indent);
		_Output.Write ("						Selectors = new () {{", _Indent);
		 separator.Reset();
		foreach  (var selector in store.Selectors) {
			_Output.Write ("{1}\n{0}", _Indent, separator);
			_Output.Write ("							new () {{\n{0}", _Indent);
			_Output.Write ("								Entries = new () {{", _Indent);
			 MakeMenuEntries2(selector.Entries);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("									}}\n{0}", _Indent);
			_Output.Write ("								}}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("							}},\n{0}", _Indent);
		_Output.Write ("						Actions = new () {{", _Indent);
		 separator.Reset();
		foreach  (var action in store.Actions) {
			_Output.Write ("{1}\n{0}", _Indent, separator);
			_Output.Write ("							new () {{\n{0}", _Indent);
			_Output.Write ("								Entries = new () {{", _Indent);
			 MakeMenuEntries2(action.Entries);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("									}}\n{0}", _Indent);
			_Output.Write ("								}}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("							}}\n{0}", _Indent);
		}

	}
