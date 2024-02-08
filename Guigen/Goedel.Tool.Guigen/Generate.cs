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
using  Goedel.Utilities;
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
		_Output.Write ("using Goedel.Utilities;\n{0}", _Indent);
		_Output.Write ("#pragma warning disable IDE1006 // Naming Styles\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("namespace {1};\n{0}", _Indent, Guigen.Class.Namespace);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#region // Sections\n{0}", _Indent);
		foreach  (var section in Guigen.Sections)  {
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for section {1} \n{0}", _Indent, section.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial class {1} : _{2} {{\n{0}", _Indent, section.Id.Label, section.Id.Label);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for section {1} \n{0}", _Indent, section.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial class _{1} : IBindable {{\n{0}", _Indent, section.Id.Label);
			_Output.Write ("\n{0}", _Indent);
			DeclareFields (section);
			CreateBindings (section);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Dialogs\n{0}", _Indent);
		foreach  (var dialog in Guigen.Dialogs)  {
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for dialog {1} \n{0}", _Indent, dialog.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial class {1} : _{2} {{\n{0}", _Indent, dialog.Id.Label, dialog.Id.Label);
			_Output.Write ("    // <summary>Type check verification.</summary>\n{0}", _Indent);
			_Output.Write ("    // public static {1} Func<object, bool> IsBacker {{ get; set; }} = (object _) => false; \n{0}", _Indent, dialog.IfSubclassNew);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for section {1} \n{0}", _Indent, dialog.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial class _{1} : {2} {{\n{0}", _Indent, dialog.Id.Label, dialog.Inherit?.Label ?? "IParameter");
			_Output.Write ("\n{0}", _Indent);
			if (  (!dialog.IsSubclass) ) {
				_Output.Write ("    public object Bound {{ get; set; }}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			DeclareFields (dialog);
			CreateBindings (dialog);
			MakeIParameterMethods (dialog);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Actions\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var action in Guigen.Actions)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for action {1} \n{0}", _Indent, action.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial class {1} : _{2} {{\n{0}", _Indent, action.Id.Label, action.Id.Label);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for action {1} \n{0}", _Indent, action.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial class _{1} : IParameter {{\n{0}", _Indent, action.Id.Label);
			_Output.Write ("\n{0}", _Indent);
			DeclareFields (action);
			CreateBindings (action);
			MakeIParameterMethods (action);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Selections\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var action in Guigen.Selections)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for action {1} \n{0}", _Indent, action.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial class {1} : _{2} {{\n{0}", _Indent, action.Id.Label, action.Id.Label);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for action {1} \n{0}", _Indent, action.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial class _{1} : IParameter {{\n{0}", _Indent, action.Id.Label);
			_Output.Write ("\n{0}", _Indent);
			DeclareFields (action);
			CreateBindings (action);
			MakeIParameterMethods (action);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Results\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var result in Guigen.Results)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Return parameters for result {1} \n{0}", _Indent, result.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial record {1} : _{2} {{\n{0}", _Indent, result.Id.Label, result.Id.Label);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for result {1} \n{0}", _Indent, result.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial record _{1} : IResult {{\n{0}", _Indent, result.Id.Label);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
			_Output.Write ("    public string Message => \"{1}\";\n{0}", _Indent, result.Message);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
			_Output.Write ("    public ResourceId ResourceId => resourceId;\n{0}", _Indent);
			_Output.Write ("    static readonly ResourceId resourceId = new (\"{1}\");\n{0}", _Indent, result.Id.Label);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<summary>The return result.</summary> \n{0}", _Indent);
			_Output.Write ("    public virtual ReturnResult ReturnResult {{ get; init; }} = ReturnResult.Completed;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			DeclareFields (result);
			CreateBindings (result);
			DeclareResultGetValues (result);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Failure Results\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var result in Guigen.Fails)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Return parameters for failure result {1} \n{0}", _Indent, result.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial record {1} : _{2} {{\n{0}", _Indent, result.Id.Label, result.Id.Label);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Callback parameters for failure result {1} \n{0}", _Indent, result.Id.Label);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public partial record _{1} : IFail {{\n{0}", _Indent, result.Id.Label);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
			_Output.Write ("    public string Message => \"{1}\";\n{0}", _Indent, result.Message);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
			_Output.Write ("    public ResourceId ResourceId => resourceId;\n{0}", _Indent);
			_Output.Write ("    static readonly ResourceId resourceId = new (\"{1}\");\n{0}", _Indent, result.Id.Label);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<summary>The return result.</summary> \n{0}", _Indent);
			_Output.Write ("    public virtual ReturnResult ReturnResult {{ get; init; }} = ReturnResult.Error;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			DeclareFields (result);
			CreateBindings (result);
			DeclareResultGetValues (result);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Gui classes\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("/// <summary>\n{0}", _Indent);
		foreach  (var text in Guigen.Class.Description) {
			_Output.Write ("/// {1}\n{0}", _Indent, text);
			}
		_Output.Write ("/// </summary>\n{0}", _Indent);
		_Output.Write ("public partial class {1} :  _{2} {{\n{0}", _Indent, Guigen.Class.Name, Guigen.Class.Name);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("/// <summary>\n{0}", _Indent);
		foreach  (var text in Guigen.Class.Description) {
			_Output.Write ("/// {1}\n{0}", _Indent, text);
			}
		_Output.Write ("/// </summary>\n{0}", _Indent);
		_Output.Write ("public class _{1} : Gui {{\n{0}", _Indent, Guigen.Class.Name);
		foreach  (var entry in Guigen.States) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<summary></summary> \n{0}", _Indent);
			_Output.Write ("    public virtual bool State{1} => true;\n{0}", _Indent, entry.Value);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/> \n{0}", _Indent);
		_Output.Write ("    public override List<GuiDialog> Dialogs {{ get; }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/> \n{0}", _Indent);
		_Output.Write ("    public override List<GuiSection> Sections {{ get; }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/> \n{0}", _Indent);
		_Output.Write ("    public override List<GuiAction> Actions {{ get; }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/> \n{0}", _Indent);
		_Output.Write ("    public override List<GuiAction> Selections {{ get; }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/> \n{0}", _Indent);
		_Output.Write ("    public override List<GuiResult> Results {{ get; }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	public override List<GuiImage> Icons => icons;\n{0}", _Indent);
		_Output.Write ("	readonly List<GuiImage> icons = new () {{ ", _Indent);
		 var separator = new Separator (",");
		foreach  (var icon in Guigen.Icons)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		new GuiImage ({1}) ", _Indent, icon.Key.Quoted());
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		}};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#region // Sections\n{0}", _Indent);
		foreach  (var section in Guigen.Sections)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<summary>Section {1}.</summary> \n{0}", _Indent, section.RecordId);
			_Output.Write ("	public GuiSection {1} {{ get; }} = new (\n{0}", _Indent, section.RecordId);
			_Output.Write ("        {1}, {2}, {3}, _{4}.BaseBinding, {5});\n{0}", _Indent, section.QuotedId, section.Prompt.Quoted(), section.Icon.Quoted(), section.IdLabel, section.Primary.If("true","false"));
			}
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Actions\n{0}", _Indent);
		foreach  (var action in Guigen.Actions)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<summary>Action {1}.</summary> \n{0}", _Indent, action.RecordId);
			_Output.Write ("	public GuiAction {1} {{ get; }} = new (\n{0}", _Indent, action.RecordId);
			_Output.Write ("        {1}, {2}, {3}, _{4}.BaseBinding, () => new {5}());\n{0}", _Indent, action.QuotedId, action.Prompt.Quoted(), action.Icon.Quoted(), action.IdLabel, action.Id.Label);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Actions\n{0}", _Indent);
		foreach  (var action in Guigen.Selections)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<summary>Selection {1}.</summary> \n{0}", _Indent, action.RecordId);
			_Output.Write ("	public GuiAction {1} {{ get; }} = new (\n{0}", _Indent, action.RecordId);
			_Output.Write ("        {1}, {2}, {3}, _{4}.BaseBinding, () => new {5}(), IsSelect:true);\n{0}", _Indent, action.QuotedId, action.Prompt.Quoted(), action.Icon.Quoted(), action.IdLabel, action.Id.Label);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Dialogs\n{0}", _Indent);
		foreach  (var dialog in Guigen.Dialogs)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<summary>Dialog {1}.</summary> \n{0}", _Indent, dialog.RecordId);
			_Output.Write ("	public GuiDialog {1} {{ get; }} = new (\n{0}", _Indent, dialog.RecordId);
			_Output.Write ("        {1}, {2}, {3}, _{4}.BaseBinding, () => new {5}()) {{\n{0}", _Indent, dialog.QuotedId, dialog.Prompt.Quoted(), dialog.Icon.Quoted(), dialog.IdLabel, dialog.Id.Label);
			_Output.Write ("                IsBoundType = (object data) => data is {1}\n{0}", _Indent, dialog.Id.Label);
			_Output.Write ("                }};\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Results\n{0}", _Indent);
		foreach  (var result in Guigen.Results)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<summary>Result {1}.</summary> \n{0}", _Indent, result.RecordId);
			_Output.Write ("	public GuiResult {1} {{ get; }} = new ();\n{0}", _Indent, result.RecordId);
			}
		_Output.Write ("	\n{0}", _Indent);
		_Output.Write ("    ///<summary>Dictionary resolving exception name to factory method.</summary> \n{0}", _Indent);
		_Output.Write ("    public Dictionary<string, Func<IResult>> ExceptionDirectory =\n{0}", _Indent);
		_Output.Write ("        new() {{", _Indent);
		 separator.Reset ();
		foreach  (var result in Guigen.Fails)  {
			foreach  (var entry in result.InheritedEntries)  {
				if (  (entry is Exception exception) ) {
					_Output.Write ("{1} \n{0}", _Indent, separator);
					_Output.Write ("                {{ typeof({1}).FullName, () => new {2}() }} ", _Indent, exception.Id, result.Id);
					}
				}
			}
		_Output.Write ("            }};\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Constructors\n{0}", _Indent);
		_Output.Write ("    /// <summary>\n{0}", _Indent);
		_Output.Write ("    /// Default constructor returning an instance.\n{0}", _Indent);
		_Output.Write ("    /// </summary>\n{0}", _Indent);
		_Output.Write ("    public _{1} () {{\n{0}", _Indent, Guigen.Class.Name);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var section in Guigen.Sections)  {
			_Output.Write ("	    {1}.Gui = this;\n{0}", _Indent, section.RecordId);
			_Output.Write ("	    {1}.Active = () => State{2};\n{0}", _Indent, section.RecordId, section.State);
			_Output.Write ("	    {1}.Entries =  new () {{ ", _Indent, section.RecordId);
			 separator.Reset ();
			foreach  (var entry in section.InheritedEntries) {
				if (  (entry.Active) ) {
					_Output.Write ("{1} \n{0}", _Indent, separator);
					GenerateEntry (entry);
					}
				}
			_Output.Write ("		    \n{0}", _Indent);
			_Output.Write ("            }};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Initialize Sections\n{0}", _Indent);
		_Output.Write ("        Sections = new List<GuiSection> () {{ ", _Indent);
		 separator.Reset ();
		foreach  (var section in Guigen.Sections)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		    {1}", _Indent, section.RecordId);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("            }};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Initialize Actions\n{0}", _Indent);
		foreach  (var action in Guigen.Actions)  {
			_Output.Write ("        {1}.Callback = (x) => {2} (x as {3}) ;\n{0}", _Indent, action.RecordId, action.Id.Label, action.Id.Label);
			_Output.Write ("	    {1}.Entries = new () {{", _Indent, action.RecordId);
			 separator.Reset ();
			foreach  (var entry in action.InheritedEntries) {
				if (  (entry.Active) ) {
					_Output.Write ("{1} \n{0}", _Indent, separator);
					GenerateEntry (entry);
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		    }};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("        Actions = new List<GuiAction>() {{ ", _Indent);
		 separator.Reset ();
		foreach  (var action in Guigen.Actions)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		    {1}", _Indent, action.RecordId);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		    }};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#region // Initialize Selections\n{0}", _Indent);
		foreach  (var action in Guigen.Selections)  {
			_Output.Write ("        {1}.Callback = (x) => {2} (x as {3}) ;\n{0}", _Indent, action.RecordId, action.Id.Label, action.DispatchType);
			_Output.Write ("	    {1}.Entries = new () {{", _Indent, action.RecordId);
			 separator.Reset ();
			foreach  (var entry in action.InheritedEntries) {
				if (  (entry.Active) ) {
					_Output.Write ("{1} \n{0}", _Indent, separator);
					GenerateEntry (entry);
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		    }};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("        Selections = new List<GuiAction>() {{ ", _Indent);
		 separator.Reset ();
		foreach  (var action in Guigen.Selections)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		    {1}", _Indent, action.RecordId);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		    }};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#region // Initialize Dialogs\n{0}", _Indent);
		foreach  (var dialog in Guigen.Dialogs)  {
			_Output.Write ("	    {1}.Entries = new () {{", _Indent, dialog.RecordId);
			 separator.Reset ();
			foreach  (var entry in dialog.InheritedEntries) {
				if (  (entry.Active) ) {
					_Output.Write ("{1} \n{0}", _Indent, separator);
					GenerateEntry (entry);
					}
				}
			_Output.Write ("			\n{0}", _Indent);
			_Output.Write ("		    }};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        //{1}.IsBacker = (object data) => {2}.IsBacker(data);\n{0}", _Indent, dialog.Id.Label, dialog.RecordId);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("        Dialogs = new List<GuiDialog>() {{ ", _Indent);
		 separator.Reset ();
		foreach  (var dialog in Guigen.Dialogs)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		    {1}", _Indent, dialog.RecordId);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		    }};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Initialize Results\n{0}", _Indent);
		foreach  (var result in Guigen.Results)  {
			_Output.Write ("	    {1}.Entries = new () {{", _Indent, result.RecordId);
			 separator.Reset ();
			foreach  (var entry in result.Entries) {
				if (  (entry.Active) ) {
					_Output.Write ("{1} \n{0}", _Indent, separator);
					GenerateEntry (entry);
					}
				}
			_Output.Write ("			\n{0}", _Indent);
			_Output.Write ("		    }};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("        Results = new List<GuiResult>() {{ ", _Indent);
		 separator.Reset ();
		foreach  (var result in Guigen.Results)  {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			_Output.Write ("		    {1}", _Indent, result.RecordId);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		    }};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("        }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("#region // Initialize Actions\n{0}", _Indent);
		foreach  (var action in Guigen.Actions)  {
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			_Output.Write ("    /// GUI action\n{0}", _Indent);
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    public virtual Task<IResult> {1} ({2} data) \n{0}", _Indent, action.Id.Label, action.Id.Label);
			_Output.Write ("                => throw new NYI();\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		foreach  (var action in Guigen.Selections)  {
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			_Output.Write ("    /// GUI action\n{0}", _Indent);
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    public virtual Task<IResult> {1} ({2} data) \n{0}", _Indent, action.Id.Label, action.DispatchType);
			_Output.Write ("                => throw new NYI();\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		_Output.Write ("	}}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#endregion\n{0}", _Indent);
		}
	

	//
	// MakeIParameterMethods
	//
	public void MakeIParameterMethods (IEntries action) {
		_Output.Write ("    ///<summary>Validation</summary> \n{0}", _Indent);
		_Output.Write ("    public {1} IResult Validate(Gui gui) {{\n{0}", _Indent, action.IfSubclassOverride);
		_Output.Write ("        GuiResultInvalid result = null;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var entry in action.AllEntries) {
			if (  (entry is IField field)  ) {
				foreach  (var fieldEntry in field.GetEntries) {
					if (  (fieldEntry is Error error)  ) {
						_Output.Write ("        // error on {1}\n{0}", _Indent, field.IdLabel);
						_Output.Write ("        if (", _Indent);
						foreach  (var t in error.Condition) {
							_Output.Write ("{1}\n{0}", _Indent, t);
							}
						_Output.Write ("            ) {{\n{0}", _Indent);
						_Output.Write ("            result ??=new GuiResultInvalid(this);\n{0}", _Indent);
						_Output.Write ("            result.SetError ({1}, \"{2}\", \"{3}\");\n{0}", _Indent, field.Index, error.Message, error.Id);
						_Output.Write ("            }}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						}
					}
				}
			}
		_Output.Write ("        return (result as IResult) ?? NullResult.Valid;\n{0}", _Indent);
		_Output.Write ("        }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<summary>Initialization.</summary> \n{0}", _Indent);
		_Output.Write ("    public {1} IResult Initialize(Gui gui) => NullResult.Initialized;\n{0}", _Indent, action.IfSubclassOverride);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<summary>Teardown.</summary> \n{0}", _Indent);
		_Output.Write ("    public {1} IResult TearDown(Gui gui) => NullResult.Teardown;\n{0}", _Indent, action.IfSubclassOverride);
		_Output.Write ("\n{0}", _Indent);
		}
	

	//
	// DeclareResultGetValues
	//
	public void DeclareResultGetValues (IEntries parent) {
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("    public object[] GetValues() => ", _Indent);
		 var separator = new Separator ("new [] { ", ",");
		foreach  (var entry in parent.AllEntries) {
			switch (entry._Tag ()) {
				case GuigenType.Text: {
				  Text text = (Text) entry; 
				_Output.Write ("{1}\n{0}", _Indent, separator);
				_Output.Write ("        {1}", _Indent, entry.IdLabel);
				break; }
				case GuigenType.Hidden: {
				  Hidden hidden = (Hidden) entry; 
				_Output.Write ("{1}\n{0}", _Indent, separator);
				_Output.Write ("        {1}", _Indent, entry.IdLabel);
			break; }
				}
			}
		if (  (separator.IsFirst) ) {
			_Output.Write ("Array.Empty<object>();\n{0}", _Indent);
			} else {
			_Output.Write ("}};", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		}
	

	//
	// DeclareFields
	//
	public void DeclareFields (IEntries parent) {
		foreach  (var entry in parent.AllEntries) {
			if (  entry.BackerType != null ) {
				_Output.Write ("    ///<summary>{1}</summary> \n{0}", _Indent, entry.Summary);
				_Output.Write ("    public virtual {1} {2} {{ get;{3}}} \n{0}", _Indent, entry.BackerType, entry.IdLabel, entry.Readonly.If("", " set;") );
				_Output.Write ("\n{0}", _Indent);
				}
			}
		}
	

	//
	// CreateBindings
	//
	public void CreateBindings (IEntries parent) {
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("    public {1} GuiBinding Binding => BaseBinding;\n{0}", _Indent, parent.IfSubclassOverride);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<summary>The binding for the data type.</summary> \n{0}", _Indent);
		_Output.Write ("    public static {1} GuiBinding BaseBinding  {{ get; }} = ", _Indent, parent.IfSubclassNew);
		CreateBindingInner (parent);
		}
	

	//
	// CreateBindingInner
	//
	public void CreateBindingInner (IEntries parent) {
		 var empty = true;
		foreach  (var entry in parent.InheritedEntries) {
			if (  entry.BindingType != null ) {
				 empty = false;
				}
			}
		_Output.Write ("new (\n{0}", _Indent);
		_Output.Write ("        (object test) => test is {1},\n{0}", _Indent, parent.IdLabelBase);
		_Output.Write ("        () => new {1}(),\n{0}", _Indent, parent.IdLabel);
		if (  empty ) {
			_Output.Write ("        Array.Empty<GuiBoundProperty>()", _Indent);
			} else {
			_Output.Write ("        new GuiBoundProperty[] {{", _Indent);
			 var separator = new Separator (",");
			 var index = 0;
			foreach  (var entry in parent.InheritedEntries) {
				if (  entry.BindingType != null ) {
					_Output.Write ("{1} \n{0}", _Indent, separator);
					_Output.Write ("            new {1} (\"{2}\"", _Indent, entry.BindingType, entry.IdLabel);
					if (  entry.PromptQuoted != null ) {
						_Output.Write (", {1}", _Indent, entry.PromptQuoted);
						}
					if (  entry.BackerType != null ) {
						 entry.Index = index++;
						_Output.Write (", (object data) => (data as {1}).{2} ", _Indent, parent.IdLabelBase, entry.IdLabel);
						if (  (entry.Readonly) ) {
							_Output.Write (", null", _Indent);
							} else {
							_Output.Write (", (object data,{1} value) => (data as {2}).{3} = value", _Indent, entry.BackerType, parent.IdLabelBase, entry.IdLabel);
							}
						} else {
						}
					if (  entry.Width != null ) {
						_Output.Write (", Width: {1}\n{0}", _Indent, entry.Width);
						}
					if (  entry is List ) {
						 var list = entry as List;
						_Output.Write (", _{1}.BaseBinding\n{0}", _Indent, list.Type.Label);
						}
					_Output.Write (")", _Indent);
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            }}", _Indent);
			}
		_Output.Write (");\n{0}", _Indent);
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
			case GuigenType.List: {
			  List list = (List) entry; 
			
			GenerateList (list);
			break; }
			case GuigenType.Selection: {
			  Selection selection = (Selection) entry; 
			
			GenerateSelection (selection);
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
			case GuigenType.Boolean: {
			  Boolean boolean = (Boolean) entry; 
			
			GenerateBoolean (boolean);
			break; }
			case GuigenType.TextArea: {
			  TextArea text = (TextArea) entry; 
			
			GenerateTextArea (text);
			break; }
			case GuigenType.Integer: {
			  Integer integer = (Integer) entry; 
			
			GenerateInteger (integer);
			break; }
			case GuigenType.QRScan: {
			  QRScan qrscan = (QRScan) entry; 
			
			GenerateQRScan (qrscan);
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
	// GenerateList
	//
	public void GenerateList (List list) {
		 var separator = new Separator (",");
		_Output.Write ("			new GuiList ({1}, {2}, {3}, {4}, {5}, new () {{", _Indent, list.QuotedId, list.Prompt.Quoted(), list.Icon.Quoted(), list.DialogType, list.Index);
		_Indent = _Indent + "\t";
		 separator.Reset ();
		foreach  (var entry in list.Entries) {
			if (  (entry.Active) ) {
				_Output.Write ("{1} \n{0}", _Indent, separator);
				GenerateEntry (entry);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			}}) ", _Indent);
		_Indent = _Indent.Remove (0,1);
		}
	

	//
	// GenerateChooser
	//
	public void GenerateChooser (Chooser chooser) {
		 var separator = new Separator (",");
		_Output.Write ("			new GuiChooser ({1}, {2}, {3}, {4}, new () {{", _Indent, chooser.QuotedId, chooser.Prompt.Quoted(), chooser.Icon.Quoted(), chooser.Index);
		_Indent = _Indent + "\t";
		 separator.Reset ();
		foreach  (var entry in chooser.Entries) {
			if (  (entry.Active) ) {
				_Output.Write ("{1} \n{0}", _Indent, separator);
				GenerateEntry (entry);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			}}) ", _Indent);
		_Indent = _Indent.Remove (0,1);
		}
	

	//
	// GenerateDialog
	//
	public void GenerateDialog (Dialog dialog) {
		 var separator = new Separator (",");
		_Output.Write ("			new GuiDialog ({1}, new () {{", _Indent, dialog.QuotedId);
		_Indent = _Indent + "\t";
		 separator.Reset ();
		foreach  (var entry in dialog.Entries) {
			_Output.Write ("{1} \n{0}", _Indent, separator);
			GenerateEntry (entry);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		    }}) ", _Indent);
		_Indent = _Indent.Remove (0,1);
		}
	

	//
	// GenerateSelection
	//
	public void GenerateSelection (Selection field) {
		_Output.Write ("			new GuiButton ({1}, {2})", _Indent, field.QuotedId, field.Target);
		}
	

	//
	// GenerateButton
	//
	public void GenerateButton (Button field) {
		_Output.Write ("			new GuiButton ({1}, {2})", _Indent, field.QuotedId, field.Target);
		}
	

	//
	// GenerateText
	//
	public void GenerateText (Text field) {
		_Output.Write ("			new GuiText ({1}, {2}, {3}, {4})", _Indent, field.QuotedId, field.Prompt.Quoted(), field.Index, field.Width.ValueOrNull());
		}
	

	//
	// GenerateBoolean
	//
	public void GenerateBoolean (Boolean field) {
		_Output.Write ("			new GuiBoolean ({1}, {2}, {3})", _Indent, field.QuotedId, field.Prompt.Quoted(), field.Index);
		}
	

	//
	// GenerateTextArea
	//
	public void GenerateTextArea (TextArea field) {
		_Output.Write ("			new GuiTextArea ({1}, {2}, {3})", _Indent, field.QuotedId, field.Prompt.Quoted(), field.Index);
		}
	

	//
	// GenerateInteger
	//
	public void GenerateInteger (Integer field) {
		_Output.Write ("			new GuiInteger ({1}, {2}, {3})", _Indent, field.QuotedId, field.Prompt.Quoted(), field.Index);
		}
	

	//
	// GenerateQRScan
	//
	public void GenerateQRScan (QRScan field) {
		_Output.Write ("			new GuiQRScan ({1}, {2}, {3})", _Indent, field.QuotedId, field.Prompt.Quoted(), field.Index);
		}
	

	//
	// GenerateColor
	//
	public void GenerateColor (Color field) {
		_Output.Write ("			new GuiColor ({1}, {2})", _Indent, field.QuotedId, field.Prompt.Quoted());
		}
	

	//
	// GenerateSize
	//
	public void GenerateSize (Size field) {
		_Output.Write ("			new GuiSize ({1}, {2})", _Indent, field.QuotedId, field.Prompt.Quoted());
		}
	

	//
	// GenerateDecimal
	//
	public void GenerateDecimal (Decimal field) {
		_Output.Write ("			new GuiDecimal ({1}, {2})", _Indent, field.QuotedId, field.Prompt.Quoted());
		}
	

	//
	// GenerateIcon
	//
	public void GenerateIcon (Icon field) {
		_Output.Write ("			new GuiIcon ({1}, {2})", _Indent, field.QuotedId, field.Prompt.Quoted());
		}
	

	//
	// GenerateResx
	//
	public void GenerateResx (Guigen Guigen) {
		_Output.Write ("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n{0}", _Indent);
		_Output.Write ("<root>\n{0}", _Indent);
		_Output.Write ("  <!-- \n{0}", _Indent);
		_Output.Write ("    Microsoft ResX Schema \n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    Version 2.0\n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    The primary goals of this format is to allow a simple XML format \n{0}", _Indent);
		_Output.Write ("    that is mostly human readable. The generation and parsing of the \n{0}", _Indent);
		_Output.Write ("    various data types are done through the TypeConverter classes \n{0}", _Indent);
		_Output.Write ("    associated with the data types.\n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    Example:\n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    ... ado.net/XML headers & schema ...\n{0}", _Indent);
		_Output.Write ("    <resheader name=\"resmimetype\">text/microsoft-resx</resheader>\n{0}", _Indent);
		_Output.Write ("    <resheader name=\"version\">2.0</resheader>\n{0}", _Indent);
		_Output.Write ("    <resheader name=\"reader\">System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>\n{0}", _Indent);
		_Output.Write ("    <resheader name=\"writer\">System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>\n{0}", _Indent);
		_Output.Write ("    <data name=\"Name1\"><value>this is my long string</value><comment>this is a comment</comment></data>\n{0}", _Indent);
		_Output.Write ("    <data name=\"Color1\" type=\"System.Drawing.Color, System.Drawing\">Blue</data>\n{0}", _Indent);
		_Output.Write ("    <data name=\"Bitmap1\" mimetype=\"application/x-microsoft.net.object.binary.base64\">\n{0}", _Indent);
		_Output.Write ("        <value>[base64 mime encoded serialized .NET Framework object]</value>\n{0}", _Indent);
		_Output.Write ("    </data>\n{0}", _Indent);
		_Output.Write ("    <data name=\"Icon1\" type=\"System.Drawing.Icon, System.Drawing\" mimetype=\"application/x-microsoft.net.object.bytearray.base64\">\n{0}", _Indent);
		_Output.Write ("        <value>[base64 mime encoded string representing a byte array form of the .NET Framework object]</value>\n{0}", _Indent);
		_Output.Write ("        <comment>This is a comment</comment>\n{0}", _Indent);
		_Output.Write ("    </data>\n{0}", _Indent);
		_Output.Write ("                \n{0}", _Indent);
		_Output.Write ("    There are any number of \"resheader\" rows that contain simple \n{0}", _Indent);
		_Output.Write ("    name/value pairs.\n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    Each data row contains a name, and value. The row also contains a \n{0}", _Indent);
		_Output.Write ("    type or mimetype. Type corresponds to a .NET class that support \n{0}", _Indent);
		_Output.Write ("    text/value conversion through the TypeConverter architecture. \n{0}", _Indent);
		_Output.Write ("    Classes that don't support this are serialized and stored with the \n{0}", _Indent);
		_Output.Write ("    mimetype set.\n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    The mimetype is used for serialized objects, and tells the \n{0}", _Indent);
		_Output.Write ("    ResXResourceReader how to depersist the object. This is currently not \n{0}", _Indent);
		_Output.Write ("    extensible. For a given mimetype the value must be set accordingly:\n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    Note - application/x-microsoft.net.object.binary.base64 is the format \n{0}", _Indent);
		_Output.Write ("    that the ResXResourceWriter will generate, however the reader can \n{0}", _Indent);
		_Output.Write ("    read any of the formats listed below.\n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    mimetype: application/x-microsoft.net.object.binary.base64\n{0}", _Indent);
		_Output.Write ("    value   : The object must be serialized with \n{0}", _Indent);
		_Output.Write ("            : System.Runtime.Serialization.Formatters.Binary.BinaryFormatter\n{0}", _Indent);
		_Output.Write ("            : and then encoded with base64 encoding.\n{0}", _Indent);
		_Output.Write ("    \n{0}", _Indent);
		_Output.Write ("    mimetype: application/x-microsoft.net.object.soap.base64\n{0}", _Indent);
		_Output.Write ("    value   : The object must be serialized with \n{0}", _Indent);
		_Output.Write ("            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter\n{0}", _Indent);
		_Output.Write ("            : and then encoded with base64 encoding.\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    mimetype: application/x-microsoft.net.object.bytearray.base64\n{0}", _Indent);
		_Output.Write ("    value   : The object must be serialized into a byte array \n{0}", _Indent);
		_Output.Write ("            : using a System.ComponentModel.TypeConverter\n{0}", _Indent);
		_Output.Write ("            : and then encoded with base64 encoding.\n{0}", _Indent);
		_Output.Write ("    -->\n{0}", _Indent);
		_Output.Write ("  <xsd:schema id=\"root\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\n{0}", _Indent);
		_Output.Write ("    <xsd:import namespace=\"http://www.w3.org/XML/1998/namespace\" />\n{0}", _Indent);
		_Output.Write ("    <xsd:element name=\"root\" msdata:IsDataSet=\"true\">\n{0}", _Indent);
		_Output.Write ("      <xsd:complexType>\n{0}", _Indent);
		_Output.Write ("        <xsd:choice maxOccurs=\"unbounded\">\n{0}", _Indent);
		_Output.Write ("          <xsd:element name=\"metadata\">\n{0}", _Indent);
		_Output.Write ("            <xsd:complexType>\n{0}", _Indent);
		_Output.Write ("              <xsd:sequence>\n{0}", _Indent);
		_Output.Write ("                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" />\n{0}", _Indent);
		_Output.Write ("              </xsd:sequence>\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute name=\"name\" use=\"required\" type=\"xsd:string\" />\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute name=\"type\" type=\"xsd:string\" />\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute name=\"mimetype\" type=\"xsd:string\" />\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute ref=\"xml:space\" />\n{0}", _Indent);
		_Output.Write ("            </xsd:complexType>\n{0}", _Indent);
		_Output.Write ("          </xsd:element>\n{0}", _Indent);
		_Output.Write ("          <xsd:element name=\"assembly\">\n{0}", _Indent);
		_Output.Write ("            <xsd:complexType>\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute name=\"alias\" type=\"xsd:string\" />\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute name=\"name\" type=\"xsd:string\" />\n{0}", _Indent);
		_Output.Write ("            </xsd:complexType>\n{0}", _Indent);
		_Output.Write ("          </xsd:element>\n{0}", _Indent);
		_Output.Write ("          <xsd:element name=\"data\">\n{0}", _Indent);
		_Output.Write ("            <xsd:complexType>\n{0}", _Indent);
		_Output.Write ("              <xsd:sequence>\n{0}", _Indent);
		_Output.Write ("                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />\n{0}", _Indent);
		_Output.Write ("                <xsd:element name=\"comment\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"2\" />\n{0}", _Indent);
		_Output.Write ("              </xsd:sequence>\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" msdata:Ordinal=\"1\" />\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute name=\"type\" type=\"xsd:string\" msdata:Ordinal=\"3\" />\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute name=\"mimetype\" type=\"xsd:string\" msdata:Ordinal=\"4\" />\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute ref=\"xml:space\" />\n{0}", _Indent);
		_Output.Write ("            </xsd:complexType>\n{0}", _Indent);
		_Output.Write ("          </xsd:element>\n{0}", _Indent);
		_Output.Write ("          <xsd:element name=\"resheader\">\n{0}", _Indent);
		_Output.Write ("            <xsd:complexType>\n{0}", _Indent);
		_Output.Write ("              <xsd:sequence>\n{0}", _Indent);
		_Output.Write ("                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />\n{0}", _Indent);
		_Output.Write ("              </xsd:sequence>\n{0}", _Indent);
		_Output.Write ("              <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" />\n{0}", _Indent);
		_Output.Write ("            </xsd:complexType>\n{0}", _Indent);
		_Output.Write ("          </xsd:element>\n{0}", _Indent);
		_Output.Write ("        </xsd:choice>\n{0}", _Indent);
		_Output.Write ("      </xsd:complexType>\n{0}", _Indent);
		_Output.Write ("    </xsd:element>\n{0}", _Indent);
		_Output.Write ("  </xsd:schema>\n{0}", _Indent);
		_Output.Write ("  <resheader name=\"resmimetype\">\n{0}", _Indent);
		_Output.Write ("    <value>text/microsoft-resx</value>\n{0}", _Indent);
		_Output.Write ("  </resheader>\n{0}", _Indent);
		_Output.Write ("  <resheader name=\"version\">\n{0}", _Indent);
		_Output.Write ("    <value>2.0</value>\n{0}", _Indent);
		_Output.Write ("  </resheader>\n{0}", _Indent);
		_Output.Write ("  <resheader name=\"reader\">\n{0}", _Indent);
		_Output.Write ("    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>\n{0}", _Indent);
		_Output.Write ("  </resheader>\n{0}", _Indent);
		_Output.Write ("  <resheader name=\"writer\">\n{0}", _Indent);
		_Output.Write ("    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>\n{0}", _Indent);
		_Output.Write ("  </resheader>\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var prompt in Guigen.Prompts) {
			_Output.Write ("  <data name=\"{1}\" xml:space=\"preserve\">\n{0}", _Indent, prompt.Value.Key);
			_Output.Write ("    <value>{1}</value>\n{0}", _Indent, prompt.Value.Text.XMLEscape());
			_Output.Write ("    <comment>Generated by Guigen</comment>\n{0}", _Indent);
			_Output.Write ("  </data>\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("</root>\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		}

	}
