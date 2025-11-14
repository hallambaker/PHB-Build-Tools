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
namespace Goedel.Tool.Sitebuilder;
public partial class GenerateBacking : global::Goedel.Registry.Script {

	 string structure = "partial class";
	
	/// <summary>	
	/// CreateFrame
	/// </summary>
	/// <param name="frameset"></param>
	public void CreateFrame (FrameSet frameset) {
		 var comma = new Registry.Separator (",");
		 var className = "MyClass";
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#pragma warning disable IDE0028 // Simplify collection initialization\n{0}", _Indent);
		_Output.Write ("namespace {1};\n{0}", _Indent, frameset.Namespace);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class FramePage : Goedel.Sitebuilder.FramePage {{\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    public FramePage(string id, string title, List<IFrameField> fields) : base(id, title, fields) {{\n{0}", _Indent);
		_Output.Write ("        }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("/// <summary>\n{0}", _Indent);
		_Output.Write ("/// Annotated backing classes for data driven GUI.\n{0}", _Indent);
		_Output.Write ("/// </summary>\n{0}", _Indent);
		_Output.Write ("public partial class {1} : FrameSet{{\n{0}", _Indent, className);
		foreach  (var backer in frameset.Pages)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	///<summary>{1}</summary>\n{0}", _Indent, backer.Id);
			_Output.Write ("	public {1} {2} {{get;}} = new();\n{0}", _Indent, backer.Id, backer.Id);
			}
		_Output.Write ("\n{0}", _Indent);
		foreach  (var backer in frameset.Menus)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	///<summary>{1}</summary>\n{0}", _Indent, backer.Id);
			_Output.Write ("	public {1} {2} {{get;}} = new();\n{0}", _Indent, backer.Id, backer.Id);
			}
		_Output.Write ("\n{0}", _Indent);
		foreach  (var backer in frameset.Selectors)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	 ///<summary>{1}</summary>\n{0}", _Indent, backer.Id);
			_Output.Write ("	 public {1} {2} {{get;}} = new();\n{0}", _Indent, backer.Id, backer.Id);
			}
		_Output.Write ("\n{0}", _Indent);
		foreach  (var backer in frameset.Classes)  {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	 ///<summary>{1}</summary>\n{0}", _Indent, backer.Id);
			_Output.Write ("	 public {1} {2} {{get;}} = new();\n{0}", _Indent, backer.Id, backer.Id);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	/// <summary>\n{0}", _Indent);
		_Output.Write ("	/// Constructor, return a new instance.\n{0}", _Indent);
		_Output.Write ("	/// </summary>\n{0}", _Indent);
		_Output.Write ("	public {1} () {{\n{0}", _Indent, className);
		_Output.Write ("\n{0}", _Indent);
		 comma.Reset();
		_Output.Write ("		Pages = [ ", _Indent);
		foreach  (var backer in frameset.Pages)  {
			_Output.Write ("{1}\n{0}", _Indent, comma);
			_Output.Write ("			{1}", _Indent, backer.Id);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			];\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		 comma.Reset();
		_Output.Write ("		Menus = [ ", _Indent);
		foreach  (var backer in frameset.Menus)  {
			_Output.Write ("{1}\n{0}", _Indent, comma);
			_Output.Write ("			{1}", _Indent, backer.Id);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			];\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		 comma.Reset();
		_Output.Write ("		Selectors = [ ", _Indent);
		foreach  (var backer in frameset.Selectors)  {
			_Output.Write ("{1}\n{0}", _Indent, comma);
			_Output.Write ("			{1}", _Indent, backer.Id);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			];\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		 comma.Reset();
		_Output.Write ("		Classes = [ ", _Indent);
		foreach  (var backer in frameset.Classes)  {
			_Output.Write ("{1}\n{0}", _Indent, comma);
			_Output.Write ("			{1}", _Indent, backer.Id);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			];\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			\n{0}", _Indent);
		_Output.Write ("		foreach (var backed in Pages) {{\n{0}", _Indent);
		_Output.Write ("			ResolveReferences (backed); \n{0}", _Indent);
		_Output.Write ("			}}\n{0}", _Indent);
		_Output.Write ("		foreach (var backed in Menus) {{\n{0}", _Indent);
		_Output.Write ("			ResolveReferences (backed); \n{0}", _Indent);
		_Output.Write ("			}}\n{0}", _Indent);
		_Output.Write ("		foreach (var backed in Selectors) {{\n{0}", _Indent);
		_Output.Write ("			ResolveReferences (backed); \n{0}", _Indent);
		_Output.Write ("			}}\n{0}", _Indent);
		_Output.Write ("		foreach (var backed in Classes) {{\n{0}", _Indent);
		_Output.Write ("			ResolveReferences (backed); \n{0}", _Indent);
		_Output.Write ("			}}\n{0}", _Indent);
		_Output.Write ("		}}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	}}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("// Pages\n{0}", _Indent);
		foreach  (var backer in frameset.Pages)  {
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Backing class for {1}\n{0}", _Indent, backer.Id);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public {1} {2} : {3} {{\n{0}", _Indent, structure, backer.Id, backer.Type);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	/// <summary>\n{0}", _Indent);
			_Output.Write ("	/// Constructor, returns a new instance\n{0}", _Indent);
			_Output.Write ("	/// </summary>\n{0}", _Indent);
			_Output.Write ("	public {1} () : base (\"{2}\", \"{3}\", _Fields) {{\n{0}", _Indent, backer.Id, backer.Id, backer.Title);
			_Output.Write ("		Container = {1};\n{0}", _Indent, backer.Container.QuotedOrNull());
			_Output.Write ("		}}\n{0}", _Indent);
			 MakeBacking (backer);
			_Output.Write ("	}}\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("// Menus \n{0}", _Indent);
		foreach  (var backer in frameset.Menus)  {
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Backing class for {1}\n{0}", _Indent, backer.Id);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public {1} {2} : {3} {{\n{0}", _Indent, structure, backer.Id, backer.Type);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// Implement factory method returning a menu bound to a specific page.\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    /// <inheritdoc/>\n{0}", _Indent);
			_Output.Write ("    public override Goedel.Sitebuilder.FramePage Page {{ \n{0}", _Indent);
			_Output.Write ("            get => page ?? FrameSet.Page;\n{0}", _Indent);
			_Output.Write ("            init {{ page = value; }} }}\n{0}", _Indent);
			_Output.Write ("    Goedel.Sitebuilder.FramePage? page = null;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    /// <inheritdoc/>\n{0}", _Indent);
			_Output.Write ("    public override {1} Create(Goedel.Sitebuilder.FramePage page) => new {2}() {{\n{0}", _Indent, backer.Id, backer.Id);
			_Output.Write ("        Page = page\n{0}", _Indent);
			_Output.Write ("        }};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	/// <summary>\n{0}", _Indent);
			_Output.Write ("	/// Constructor, returns a new instance\n{0}", _Indent);
			_Output.Write ("	/// </summary>\n{0}", _Indent);
			_Output.Write ("	public {1} () : base (\"{2}\", _Fields) {{\n{0}", _Indent, backer.Id, backer.Id);
			_Output.Write ("		}}\n{0}", _Indent);
			 MakeBacking (backer);
			_Output.Write ("	}}\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("// Classes \n{0}", _Indent);
		foreach  (var backer in frameset.Selectors)  {
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Backing class for {1}\n{0}", _Indent, backer.Id);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public {1} {2} : {3} {{\n{0}", _Indent, structure, backer.Id, backer.Type);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	/// <summary>\n{0}", _Indent);
			_Output.Write ("	/// Constructor, returns a new instance\n{0}", _Indent);
			_Output.Write ("	/// </summary>\n{0}", _Indent);
			_Output.Write ("	public {1} () : base (\"{2}\", _Fields) {{\n{0}", _Indent, backer.Id, backer.Id);
			_Output.Write ("		}}\n{0}", _Indent);
			 MakeBacking (backer);
			_Output.Write ("	}}\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("// Classes \n{0}", _Indent);
		foreach  (var backer in frameset.Classes)  {
			_Output.Write ("/// <summary>\n{0}", _Indent);
			_Output.Write ("/// Backing class for {1}\n{0}", _Indent, backer.Id);
			_Output.Write ("/// </summary>\n{0}", _Indent);
			_Output.Write ("public {1} {2} (string Id) : {3} (Id) {{\n{0}", _Indent, structure, backer.Id, backer.ParentId ?? backer.Type);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    /// <inheritdoc/>\n{0}", _Indent);
			_Output.Write ("    public override List<IFrameField> Fields {{ get; set; }} = _Fields;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	/// <summary>\n{0}", _Indent);
			_Output.Write ("	/// Paramaterless constructor enabling use of new().\n{0}", _Indent);
			_Output.Write ("	/// </summary>\n{0}", _Indent);
			_Output.Write ("	public {1}() : this(\"{2}\") {{ \n{0}", _Indent, backer.Id, backer.Id);
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			 var defaultPresentation = GetDefaultPresentation(backer.Fields);
			if (  (defaultPresentation is not null) ) {
				_Output.Write ("    /// <inheritdoc/>\n{0}", _Indent);
				_Output.Write ("    public override FramePresentation Presentation => {1};\n{0}", _Indent, defaultPresentation.Id);
				}
			_Output.Write ("\n{0}", _Indent);
			 MakeBacking (backer);
			_Output.Write ("	}}\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		}
	
	/// <summary>	
	/// MakeBacking
	/// </summary>
	/// <param name="backed"></param>
	public void MakeBacking (IBacked backed) {
		 var parentName = backed.Parent is null ? "null" : $"{backed.Parent.Id}._binding";
		 var comma = new Registry.Separator (",");
		foreach  (var entry in backed.Fields)  {
			if (  (entry.Backing != null)  ) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("    /// <summary>Field {1}</summary>\n{0}", _Indent, entry.Id);
				_Output.Write ("	public {1}? {2} {{get; set;}}\n{0}", _Indent, entry.Backing, entry.Id);
				} else if (  (entry is FrameAvatar avatar) ) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	///<summary>Avatar {1}</summary>\n{0}", _Indent, avatar.Id);
				_Output.Write ("	public string? {1} {{get; set;}}\n{0}", _Indent, avatar.Id);
				} else if (  (entry is FrameRefClass refClass) ) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	///<summary>class {1}, {2}</summary>\n{0}", _Indent, refClass.Backing, refClass.Id);
				_Output.Write ("	public {1}? {2} {{get; set;}}\n{0}", _Indent, refClass.Backing, refClass.Id);
				} else if (  (entry is FrameRefList refList) ) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	///<summary>List {1}</summary>\n{0}", _Indent, refList.Id);
				_Output.Write ("	public {1}? {2} {{get; set;}}\n{0}", _Indent, refList.Backing, refList.Id);
				} else if (  (entry is FrameRefForm refForm) ) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	///<summary>List {1}</summary>\n{0}", _Indent, refForm.Id);
				_Output.Write ("	public {1}? {2} {{get; set;}}\n{0}", _Indent, refForm.Backing, refForm.Id);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		foreach  (var entry in backed.Fields)  {
			if (  (entry is FramePresentation presentation)  ) {
				 var storeId = presentation.Id.Uniqueify();
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/// <summary>\n{0}", _Indent);
				_Output.Write ("	/// Presentation style {1}\n{0}", _Indent, presentation.Id);
				_Output.Write ("	/// </summary>\n{0}", _Indent);
				_Output.Write ("	public static FramePresentation {1} => {2} ?? new FramePresentation (\"{3}\") {{\n{0}", _Indent, presentation.Id, storeId, presentation.Id);
				_Output.Write ("		GetUid = (data) => (data as {1})?.{2},\n{0}", _Indent, backed.Id, presentation.UidField);
				_Output.Write ("		Sections = [", _Indent);
				 comma.Reset();
				foreach  (var section in presentation.Sections) {
					_Output.Write ("{1}\n{0}", _Indent, comma);
					_Output.Write ("			new FrameSection (\"{1}\") {{\n{0}", _Indent, section.Id);
					_Output.Write ("				Fields = [", _Indent);
					 var save = Indent (12);
					 RenderFields (backed.Id, section.Fields);
					 RestoreIndent (save);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("					]\n{0}", _Indent);
					_Output.Write ("				}}", _Indent);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("			]\n{0}", _Indent);
				_Output.Write ("		}}.CacheValue(out {1})!;\n{0}", _Indent, storeId);
				_Output.Write ("	static FramePresentation? {1};\n{0}", _Indent, storeId);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	static readonly List<IFrameField> _Fields = [", _Indent);
		 RenderAllFields(backed, backed.Fields, true);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		];\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    /// <inheritdoc/>\n{0}", _Indent);
		_Output.Write ("	public override Goedel.Protocol.Property[] _Properties => _properties;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<summary>Binding</summary> \n{0}", _Indent);
		_Output.Write ("	static readonly Goedel.Protocol.Property[] _properties = [\n{0}", _Indent);
		_Output.Write ("		// Only inclue the serialized items here\n{0}", _Indent);
		 var dictionary = new Dictionary<string,int>();
		 var index = 0;
		 comma.Reset();
		foreach  (var entry in backed.Fields)  {
			if (  entry is Goedel.Protocol.Property property ) {
				 dictionary.Add (property.Tag, index++);
				 RenderField (backed.Id, entry, comma);
				}
			}
		_Output.Write ("		];\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    /// <inheritdoc/>\n{0}", _Indent);
		_Output.Write ("	public override Binding _Binding => _binding;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<summary>Binding</summary> \n{0}", _Indent);
		_Output.Write ("	protected static readonly Binding<{1}> _binding = new (\n{0}", _Indent, backed.Id);
		_Output.Write ("			new() {{\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			// Only inclue the serialized items here", _Indent);
		 comma.Reset();
		foreach  (var entry in backed.Fields)  {
			if (  entry is Goedel.Protocol.Property property ) {
				if (  (dictionary.TryGetValue (property.Tag, out var i)) ) {
					_Output.Write ("{1}\n{0}", _Indent, comma);
					_Output.Write ("			{{\"{1}\", _properties[{2}]}}", _Indent, property.Tag, i);
					}
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			}}, \"{1}\",\n{0}", _Indent, backed.Tag);
		_Output.Write ("		() => new {1}(), () => [], () => [], Parent: {2}, Generic: false);\n{0}", _Indent, backed.Tag, parentName);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		}
	
	/// <summary>	
	/// RenderAllFields
	/// </summary>
	/// <param name="backed"></param>
	/// <param name="fields"></param>
	/// <param name="parent=false"></param>
	public void RenderAllFields (IBacked backed, List<IFrameField> fields, bool parent=false) {
		 var comma = new Registry.Separator (",");
		if (  (parent && backed.Parent is not null)  ) {
			_Output.Write ("{1}", _Indent, comma);
			 RenderAllFields(backed.Parent, backed.Parent.Fields);
			}
		foreach  (var entry in fields)  {
			 RenderField (backed.Id, entry, comma);
			}
		}
	
	/// <summary>	
	/// RenderFields
	/// </summary>
	/// <param name="backedId"></param>
	/// <param name="fields"></param>
	/// <param name="parent=false"></param>
	public void RenderFields (string backedId, List<IFrameField> fields, bool parent=false) {
		 var comma = new Registry.Separator (",");
		foreach  (var entry in fields)  {
			 RenderField (backedId, entry, comma);
			}
		}
	
	/// <summary>	
	/// RenderField
	/// </summary>
	/// <param name="backed"></param>
	/// <param name="entry"></param>
	/// <param name="comma"></param>
	public void RenderField (string backed, IFrameField entry, Registry.Separator comma) {
		 var id = entry.Id.Replace (".", "?.");
		 var sid = entry.Id.Replace (".", "!.");
		 switch (entry) {
		 case FrameButtonParsed button: {
		 var comma2 = new Registry.Separator (",");
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameButton (\"{1}\", \"{2}\", \"{3}\") {{", _Indent, entry.Id, button.Label, button.Action);
		if (  (button.Active is not null)  ) {
			 var bid = button.Active.Replace (".", "?.");
			_Output.Write ("{1}\n{0}", _Indent, comma2);
			_Output.Write ("			GetActive = (data) => (data as {1})?.{2}", _Indent, backed, bid);
			}
		if (  (button.Integer is not null)  ) {
			 var bid = button.Integer.Replace (".", "?.");
			_Output.Write ("{1}\n{0}", _Indent, comma2);
			_Output.Write ("			GetInteger = (data) => (data as {1})?.{2}", _Indent, backed, bid);
			}
		if (  (button.Text is not null)  ) {
			 var bid = button.Text.Replace (".", "?.");
			_Output.Write ("{1}\n{0}", _Indent, comma2);
			_Output.Write ("			GetText = (data) => (data as {1})?.{2}", _Indent, backed, bid);
			}
		if (  (button.Description is not null)  ) {
			_Output.Write ("{1}\n{0}", _Indent, comma2);
			_Output.Write ("			Description = \"{1}\"", _Indent, button.Description);
			}
		if (  (button.ButtonAction != ButtonAction.Link)  ) {
			_Output.Write ("{1}\n{0}", _Indent, comma2);
			_Output.Write ("			ButtonAction = ButtonAction.{1}", _Indent, button.ButtonAction);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			}}", _Indent);
		 break; }
		 case FrameRefMenu reference: {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameRefMenu (\"{1}\",\"{2}\")", _Indent, entry.Id, reference.Reference);
		 break; }
		 case FrameAvatar avatar: {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameAvatar (\"{1}\"){{\n{0}", _Indent, entry.Id);
		_Output.Write ("			Prompt = {1},\n{0}", _Indent, entry.Prompt.QuotedOrNull());
		_Output.Write ("			Get = (data) => (data as {1})?.{2} }}", _Indent, backed, id);
		 break; }
		 case FrameFile file: {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameFile (\"{1}\"){{\n{0}", _Indent, entry.Id);
		_Output.Write ("			FileType = {1},\n{0}", _Indent, file.FileType.QuotedOrNull());
		_Output.Write ("			Prompt = {1},\n{0}", _Indent, entry.Prompt.QuotedOrNull());
		_Output.Write ("			Description = {1},\n{0}", _Indent, entry.Description.QuotedOrNull());
		_Output.Write ("			Get = (data) => (data as {1})?.{2} ,\n{0}", _Indent, backed, id);
		_Output.Write ("			Set = (data, value) => {{(data as {1})!.{2} = value as {3}; }}}}", _Indent, backed, sid, file.Backing);
		 break; }
		 case FrameRefClass reference: {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameRefClass<{1}> (\"{2}\",\"{3}\"){{\n{0}", _Indent, reference.Backing, entry.Id, reference.Reference);
		if (  reference.PresentationId is not null ) {
			_Output.Write ("			Presentation = {1},\n{0}", _Indent, reference.PresentationId);
			}
		_Output.Write ("			Get = (data) => (data as {1})?.{2} ,\n{0}", _Indent, backed, id);
		_Output.Write ("			Set = (data, value) => {{(data as {1})!.{2} = value as {3}; }}}}", _Indent, backed, sid, reference.Reference);
		 break; }
		 case FrameRefList reference: {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameRefList<{1}> (\"{2}\",\"{3}\"){{\n{0}", _Indent, reference.Reference, entry.Id, reference.Reference);
		if (  reference.PresentationId is not null ) {
			_Output.Write ("			Presentation = {1},\n{0}", _Indent, reference.PresentationId);
			}
		_Output.Write ("			Get = (data) => (data as {1})?.{2} ,\n{0}", _Indent, backed, id);
		_Output.Write ("			Set = (data, value) => {{(data as {1})!.{2} = value as List<{3}>; }}}}", _Indent, backed, sid, reference.Reference);
		 break; }
		 case FrameRefForm reference: {
		 var comma2 = new Registry.Separator(",");
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameRefForm<{1}> (\"{2}\",\"{3}\", [", _Indent, reference.Reference, entry.Id, reference.Reference);
		foreach  (var formentry in reference.Fields) {
			 RenderField (reference.Reference, formentry, comma2);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("				]){{\n{0}", _Indent);
		if (  reference.PresentationId is not null ) {
			_Output.Write ("			Presentation = {1},\n{0}", _Indent, reference.PresentationId);
			}
		_Output.Write ("			Get = (data) => (data as {1})?.{2} ,\n{0}", _Indent, backed, id);
		_Output.Write ("			Set = (data, value) => {{(data as {1})!.{2} = value as {3}; }}}}", _Indent, backed, sid, reference.Reference);
		 break; }
		 case FrameRef : {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameRef (\"{1}\")", _Indent, entry.Id);
		 break; }
		 case FrameSeparator : {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameSeparator (\"{1}\")", _Indent, entry.Id);
		 break; }
		 case FrameIcon : {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameIcon (\"{1}\")", _Indent, entry.Id);
		 break; }
		 case FramePresentation presentation: {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		{1}", _Indent, presentation.Id);
		 break; }
		 case FrameSubmenu submenu: {
		_Output.Write ("{1}\n{0}", _Indent, comma);
		_Output.Write ("		new FrameSubmenu (\"{1}\", \"{2}\") {{\n{0}", _Indent, submenu.Id, submenu.Label);
		_Output.Write ("			Fields = [", _Indent);
		 var save = Indent (8);
		 RenderFields (backed, submenu.Fields);
		 RestoreIndent (save);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("				]\n{0}", _Indent);
		_Output.Write ("			}}", _Indent);
		 break; }
		 default: {
		if (  entry.Backing != null ) {
			_Output.Write ("{1}\n{0}", _Indent, comma);
			_Output.Write ("		new {1} (\"{2}\",\n{0}", _Indent, entry.Type, entry.Tag);
			_Output.Write ("			(data, value) => {{(data as {1})!.{2} = value; }},\n{0}", _Indent, backed, sid);
			_Output.Write ("			(data) => (data as {1})?.{2}) {{", _Indent, backed, id);
			 var comma3 = new Registry.Separator (",");
			if (  entry.Prompt is not null ) {
				_Output.Write ("{1}\n{0}", _Indent, comma3);
				_Output.Write ("				Prompt = \"{1}\"", _Indent, entry.Prompt);
				}
			if (  entry.Hidden ) {
				_Output.Write ("{1}\n{0}", _Indent, comma3);
				_Output.Write ("				Hidden = true", _Indent);
				}
			if (  entry.Description is not null ) {
				_Output.Write ("{1}\n{0}", _Indent, comma3);
				_Output.Write ("				Description = \"{1}\"", _Indent, entry.Description);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("				}}", _Indent);
			}
		 break; }
		 }
		}

	}
