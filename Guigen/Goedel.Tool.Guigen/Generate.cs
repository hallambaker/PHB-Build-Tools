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
		_Output.Write ("using Goedel.Guigen;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("namespace {1};\n{0}", _Indent, Guigen.Class.Namespace);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("/// <summary>\n{0}", _Indent);
		foreach  (var text in Guigen.Class.Description) {
			_Output.Write ("/// {1}\n{0}", _Indent, text);
			}
		_Output.Write ("/// </summary>\n{0}", _Indent);
		_Output.Write ("public partial class {1} : Gui {{\n{0}", _Indent, Guigen.Class.Name);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("	public override List<GuiIcon> Icons => icons;\n{0}", _Indent);
		_Output.Write ("	readonly List<GuiIcon> icons = new () {{ ", _Indent);
		 var separator = new Separator (",");
		foreach  (var icon in Guigen.Icons)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		new GuiIcon (\"{1}\") ", _Indent, icon.Key);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		}};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("	public override List<GuiSection> Sections => sections;\n{0}", _Indent);
		_Output.Write ("	readonly List<GuiSection> sections = new () {{ ", _Indent);
		 separator.Reset ();
		foreach  (var section in Guigen.Sections)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		{1}", _Indent, section.RecordId);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		}};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("	public override List<GuiAction> Actions => actions;\n{0}", _Indent);
		_Output.Write ("	readonly List<GuiAction> actions = new () {{ ", _Indent);
		 separator.Reset ();
		foreach  (var action in Guigen.Actions)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		{1}", _Indent, action.RecordId);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		}};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("	public override List<GuiDialog> Dialogs => dialogs;\n{0}", _Indent);
		_Output.Write ("	readonly List<GuiDialog> dialogs = new () {{ ", _Indent);
		 separator.Reset ();
		foreach  (var dialog in Guigen.Dialogs)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		{1}", _Indent, dialog.RecordId);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		}};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	// Sections\n{0}", _Indent);
		foreach  (var section in Guigen.Sections)  {
			_Output.Write ("	static readonly GuiSection {1} = new (\n{0}", _Indent, section.RecordId);
			_Output.Write ("			\"{1}\", \"{2}\", \"{3}\", {4}, new List<ISectionEntry>() {{ ", _Indent, section.Id, section.Prompt, section.Icon, section.Primary.If("true","false"));
			 separator.Reset ();
			foreach  (var entry in section.Entries) {
				if (  (entry.Active) ) {
					_Output.Write ("{1} \n{0}", _Indent, separator);
					GenerateEntry (entry);
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			}}) {{\n{0}", _Indent);
			_Output.Write ("		}};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("	\n{0}", _Indent);
		_Output.Write ("	// Actions\n{0}", _Indent);
		foreach  (var action in Guigen.Actions)  {
			_Output.Write ("	static readonly GuiAction {1} = new (\n{0}", _Indent, action.RecordId);
			_Output.Write ("			\"{1}\", \"{2}\", \"{3}\", new List<IActionEntry>() {{", _Indent, action.Id, action.Prompt, action.Icon);
			 separator.Reset ();
			foreach  (var entry in action.Entries) {
				_Output.Write ("{1} \n{0}", _Indent, separator);
				GenerateEntry (entry);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			}}) {{\n{0}", _Indent);
			_Output.Write ("		}};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	// Dialogs\n{0}", _Indent);
		foreach  (var dialog in Guigen.Dialogs)  {
			_Output.Write ("	static readonly GuiDialog {1} = new (\n{0}", _Indent, dialog.RecordId);
			_Output.Write ("			\"{1}\", new List<IDialogEntry>() {{", _Indent, dialog.Id);
			 separator.Reset ();
			foreach  (var entry in dialog.Entries) {
				_Output.Write ("{1} \n{0}", _Indent, separator);
				GenerateEntry (entry);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			}}) {{\n{0}", _Indent);
			_Output.Write ("		}};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("	\n{0}", _Indent);
		_Output.Write ("	}}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		}
	

	//
	// GenerateEntry
	//
	public void GenerateEntry (_Choice entry) {
		switch (entry._Tag ()) {
			case GuigenType.Chooser: {
			  Chooser chooser = (Chooser) entry; 
			
			GenerateChooser (chooser);
			break; }
			case GuigenType.Button: {
			  Button button = (Button) entry; 
			
			GenerateButton (button);
			break; }
			case GuigenType.Dialog: {
			  Dialog dialog = (Dialog) entry; 
			
			GenerateDialog (dialog);
			break; }
			case GuigenType.Text: {
			  Text text = (Text) entry; 
			
			GenerateText (text);
			break; }
			case GuigenType.Color: {
			  Color color = (Color) entry; 
			
			GenerateColor (color);
			break; }
			case GuigenType.Size: {
			  Size size = (Size) entry; 
			
			GenerateSize (size);
			break; }
			case GuigenType.Decimal: {
			  Decimal decimalv = (Decimal) entry; 
			
			GenerateDecimal (decimalv);
			break; }
			case GuigenType.Icon: {
			  Icon icon = (Icon) entry; 
			
			GenerateIcon (icon);
		break; }
			}
		}
	

	//
	// GenerateChooser
	//
	public void GenerateChooser (Chooser chooser) {
		_Output.Write ("			// Chooser ", _Indent);
		}
	

	//
	// GenerateButton
	//
	public void GenerateButton (Button button) {
		_Output.Write ("			// Button ", _Indent);
		}
	

	//
	// GenerateDialog
	//
	public void GenerateDialog (Dialog dialog) {
		_Output.Write ("			// Dialog ", _Indent);
		}
	

	//
	// GenerateText
	//
	public void GenerateText (Text text) {
		_Output.Write ("			// Text ", _Indent);
		}
	

	//
	// GenerateColor
	//
	public void GenerateColor (Color color) {
		_Output.Write ("			// Color ", _Indent);
		}
	

	//
	// GenerateSize
	//
	public void GenerateSize (Size size) {
		_Output.Write ("			// Text ", _Indent);
		}
	

	//
	// GenerateDecimal
	//
	public void GenerateDecimal (Decimal decimalv) {
		_Output.Write ("			// Decimal ", _Indent);
		}
	

	//
	// GenerateIcon
	//
	public void GenerateIcon (Icon icon) {
		_Output.Write ("			// Icon ", _Indent);
		}

	}
