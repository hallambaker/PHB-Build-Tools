// #script 1.0 
// Script Syntax Version:  1.0
// #license MITLicense 

//  Copyright ©  2011 by Default Deny Security Inc.
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
// #using System.Linq 
using  System.Linq;
// #pclass Goedel.Trojan GenerateWpf 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Trojan {
	public partial class GenerateWpf : global::Goedel.Registry.Script {
		public GenerateWpf () : base () {
			}
		public GenerateWpf (TextWriter Output) : base (Output) {
			}

		// #! 
		//
		// #% DateTime GenerateTime = DateTime.UtcNow; 
		 DateTime GenerateTime = DateTime.UtcNow;
		//  
		// #method Generate GUISchema GUISchema 
		

		//
		// Generate
		//
		public void Generate (GUISchema GUISchema) {
			//  
			_Output.Write ("\n{0}", _Indent);
			// The files in this directory have been generated automatically using 
			_Output.Write ("The files in this directory have been generated automatically using\n{0}", _Indent);
			// the TROJAN GUI builder. 
			_Output.Write ("the TROJAN GUI builder.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		//  
		// #method GenerateXAML Dialog Dialog 
		

		//
		// GenerateXAML
		//
		public void GenerateXAML (Dialog Dialog) {
			// #% var Wizard = Dialog.Wizard; 
			 var Wizard = Dialog.Wizard;
			// #% var GUI = Dialog.GUI; 
			 var GUI = Dialog.GUI;
			// <Page x:Class="#{GUI.Namespace}.Dialog_#{Dialog.Id}" 
			_Output.Write ("<Page x:Class=\"{1}.Dialog_{2}\"\n{0}", _Indent, GUI.Namespace, Dialog.Id);
			//       xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
			_Output.Write ("      xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\n{0}", _Indent);
			//       xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" 
			_Output.Write ("      xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\n{0}", _Indent);
			//       xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"  
			_Output.Write ("      xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" \n{0}", _Indent);
			//       xmlns:d="http://schemas.microsoft.com/expression/blend/2008"  
			_Output.Write ("      xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\" \n{0}", _Indent);
			//       mc:Ignorable="d"  
			_Output.Write ("      mc:Ignorable=\"d\" \n{0}", _Indent);
			//       d:DesignHeight="600" d:DesignWidth="800" 
			_Output.Write ("      d:DesignHeight=\"600\" d:DesignWidth=\"800\"\n{0}", _Indent);
			// 	Title="Dialog_#{Dialog.Id}"> 
			_Output.Write ("	Title=\"Dialog_{1}\">\n{0}", _Indent, Dialog.Id);
			//     <DockPanel> 
			_Output.Write ("    <DockPanel>\n{0}", _Indent);
			//         <TextBlock TextWrapping="WrapWithOverflow" FontSize="32"  
			_Output.Write ("        <TextBlock TextWrapping=\"WrapWithOverflow\" FontSize=\"32\" \n{0}", _Indent);
			//             xml:space="preserve" Margin="25,25,25,0" DockPanel.Dock="Top">#! 
			_Output.Write ("            xml:space=\"preserve\" Margin=\"25,25,25,0\" DockPanel.Dock=\"Top\">", _Indent);
			// #foreach (var Heading in Dialog.Entries.OfType<Heading>()) 
			foreach  (var Heading in Dialog.Entries.OfType<Heading>()) {
				// #foreach (var TextString in Heading.Data) 
				foreach  (var TextString in Heading.Data) {
					// #{TextString} 
					_Output.Write ("{1}\n{0}", _Indent, TextString);
					// #end foreach 
					}
				// #end foreach 
				}
			// </TextBlock> 
			_Output.Write ("</TextBlock>\n{0}", _Indent);
			//         <TextBlock TextWrapping="WrapWithOverflow" FontSize="20"  
			_Output.Write ("        <TextBlock TextWrapping=\"WrapWithOverflow\" FontSize=\"20\" \n{0}", _Indent);
			//             xml:space="preserve" Margin="25,0,25,25" DockPanel.Dock="Top">#! 
			_Output.Write ("            xml:space=\"preserve\" Margin=\"25,0,25,25\" DockPanel.Dock=\"Top\">", _Indent);
			// #foreach (var Text in Dialog.Entries.OfType<Text>()) 
			foreach  (var Text in Dialog.Entries.OfType<Text>()) {
				// #foreach (var TextString in Text.Data) 
				foreach  (var TextString in Text.Data) {
					// #{TextString} 
					_Output.Write ("{1}\n{0}", _Indent, TextString);
					// #end foreach 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end foreach 
				}
			// </TextBlock> 
			_Output.Write ("</TextBlock>\n{0}", _Indent);
			//         <StackPanel> 
			_Output.Write ("        <StackPanel>\n{0}", _Indent);
			// #foreach (var Entry in Dialog.Entries) 
			foreach  (var Entry in Dialog.Entries) {
				// #switchcast GUISchemaType Entry 
				switch (Entry._Tag ()) {
					// #casecast Input Input 
					case GUISchemaType.Input: {
					  Input Input = (Input) Entry; 
					//             <StackPanel Orientation="Horizontal"> 
					_Output.Write ("            <StackPanel Orientation=\"Horizontal\">\n{0}", _Indent);
					// 				<Label FontSize="20" 
					_Output.Write ("				<Label FontSize=\"20\"\n{0}", _Indent);
					// 					   VerticalAlignment="Top">#{Input.Label}</Label> 
					_Output.Write ("					   VerticalAlignment=\"Top\">{1}</Label>\n{0}", _Indent, Input.Label);
					// 				<TextBox FontSize="20" Name="Input_#{Input.Id}" 
					_Output.Write ("				<TextBox FontSize=\"20\" Name=\"Input_{1}\"\n{0}", _Indent, Input.Id);
					// 						 VerticalAlignment="Top" TextChanged="Changed_#{Input.XID}" Width="400"  
					_Output.Write ("						 VerticalAlignment=\"Top\" TextChanged=\"Changed_{1}\" Width=\"400\" \n{0}", _Indent, Input.XID);
					// 						 ></TextBox> 
					_Output.Write ("						 ></TextBox>\n{0}", _Indent);
					// 			</StackPanel> 
					_Output.Write ("			</StackPanel>\n{0}", _Indent);
					// #casecast Output Output 
					break; }
					case GUISchemaType.Output: {
					  Output Output = (Output) Entry; 
					//             <StackPanel Orientation="Horizontal"> 
					_Output.Write ("            <StackPanel Orientation=\"Horizontal\">\n{0}", _Indent);
					// 				<Label FontSize="20" 
					_Output.Write ("				<Label FontSize=\"20\"\n{0}", _Indent);
					// 					   VerticalAlignment="Top">#{Output.Label}</Label> 
					_Output.Write ("					   VerticalAlignment=\"Top\">{1}</Label>\n{0}", _Indent, Output.Label);
					// 				<TextBlock FontSize="20" Name="Output_#{Output.Id}" 
					_Output.Write ("				<TextBlock FontSize=\"20\" Name=\"Output_{1}\"\n{0}", _Indent, Output.Id);
					// 						 VerticalAlignment="Top"></TextBlock> 
					_Output.Write ("						 VerticalAlignment=\"Top\"></TextBlock>\n{0}", _Indent);
					// 			</StackPanel> 
					_Output.Write ("			</StackPanel>\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Horizontal in Dialog.Entries.OfType<Horizontal>()) 
			foreach  (var Horizontal in Dialog.Entries.OfType<Horizontal>()) {
				// #% int Column = 0; 
				 int Column = 0;
				//         <Grid Margin="20,20,20,20"> 
				_Output.Write ("        <Grid Margin=\"20,20,20,20\">\n{0}", _Indent);
				//             <Grid.ColumnDefinitions> 
				_Output.Write ("            <Grid.ColumnDefinitions>\n{0}", _Indent);
				// #foreach (var Action in Horizontal.Entries.OfType<Action>()) 
				foreach  (var Action in Horizontal.Entries.OfType<Action>()) {
					// #if (Column++ > 0) 
					if (  (Column++ > 0) ) {
						//                 <ColumnDefinition Width="5"/> 
						_Output.Write ("                <ColumnDefinition Width=\"5\"/>\n{0}", _Indent);
						// #end if 
						}
					//                 <ColumnDefinition/> 
					_Output.Write ("                <ColumnDefinition/>\n{0}", _Indent);
					// #end foreach 
					}
				//             </Grid.ColumnDefinitions> 
				_Output.Write ("            </Grid.ColumnDefinitions>\n{0}", _Indent);
				// #% Column = 0; 
				 Column = 0;
				// #foreach (var Action in Horizontal.Entries.OfType<Action>()) 
				foreach  (var Action in Horizontal.Entries.OfType<Action>()) {
					// 			<StackPanel Grid.Column="#{Column}"> 
					_Output.Write ("			<StackPanel Grid.Column=\"{1}\">\n{0}", _Indent, Column);
					//                 <Button  FontSize="20" Width="150" Height="30"  
					_Output.Write ("                <Button  FontSize=\"20\" Width=\"150\" Height=\"30\" \n{0}", _Indent);
					//                          Click="Action_#{Action.Id}">#{Action.Label}</Button> 
					_Output.Write ("                         Click=\"Action_{1}\">{2}</Button>\n{0}", _Indent, Action.Id, Action.Label);
					// #foreach (var Text in Action.Entries.OfType<Text>()) 
					foreach  (var Text in Action.Entries.OfType<Text>()) {
						//                 <TextBlock TextWrapping="Wrap" FontSize="16"   TextAlignment="Center" 
						_Output.Write ("                <TextBlock TextWrapping=\"Wrap\" FontSize=\"16\"   TextAlignment=\"Center\"\n{0}", _Indent);
						// 						VerticalAlignment="Center"  Margin="20,20,20,20">#! 
						_Output.Write ("						VerticalAlignment=\"Center\"  Margin=\"20,20,20,20\">", _Indent);
						// #foreach (var TextString in Text.Data) 
						foreach  (var TextString in Text.Data) {
							// #{TextString} 
							_Output.Write ("{1}\n{0}", _Indent, TextString);
							// #end foreach 
							}
						// </TextBlock> 
						_Output.Write ("</TextBlock>\n{0}", _Indent);
						// #end foreach 
						}
					//             </StackPanel> 
					_Output.Write ("            </StackPanel>\n{0}", _Indent);
					// #% Column += 2; 
					 Column += 2;
					// #end foreach  
					}
				// 		</Grid> 
				_Output.Write ("		</Grid>\n{0}", _Indent);
				// #end foreach 
				}
			// #foreach (var Vertical in Dialog.Entries.OfType<Vertical>()) 
			foreach  (var Vertical in Dialog.Entries.OfType<Vertical>()) {
				// #% int Row = 0; 
				 int Row = 0;
				//         <Grid> 
				_Output.Write ("        <Grid>\n{0}", _Indent);
				//             <Grid.RowDefinitions> 
				_Output.Write ("            <Grid.RowDefinitions>\n{0}", _Indent);
				// #foreach (var Entry in Vertical.Entries) 
				foreach  (var Entry in Vertical.Entries) {
					// #switchcast GUISchemaType Entry 
					switch (Entry._Tag ()) {
						// #casecast Action Action 
						case GUISchemaType.Action: {
						  Action Action = (Action) Entry; 
						//                 <RowDefinition /> 
						_Output.Write ("                <RowDefinition />\n{0}", _Indent);
						// #casecast Task Task 
						break; }
						case GUISchemaType.Task: {
						  Task Task = (Task) Entry; 
						//                 <RowDefinition /> 
						_Output.Write ("                <RowDefinition />\n{0}", _Indent);
						// #casecast Input Input 
						break; }
						case GUISchemaType.Input: {
						  Input Input = (Input) Entry; 
						//                 <RowDefinition /> 
						_Output.Write ("                <RowDefinition />\n{0}", _Indent);
						// #casecast Output Output 
						break; }
						case GUISchemaType.Output: {
						  Output Output = (Output) Entry; 
						//                 <RowDefinition /> 
						_Output.Write ("                <RowDefinition />\n{0}", _Indent);
						// #end switchcast 
					break; }
						}
					// #end foreach 
					}
				//             </Grid.RowDefinitions> 
				_Output.Write ("            </Grid.RowDefinitions>\n{0}", _Indent);
				//             <Grid.ColumnDefinitions> 
				_Output.Write ("            <Grid.ColumnDefinitions>\n{0}", _Indent);
				//                 <ColumnDefinition Width="Auto"/> 
				_Output.Write ("                <ColumnDefinition Width=\"Auto\"/>\n{0}", _Indent);
				//                 <ColumnDefinition/> 
				_Output.Write ("                <ColumnDefinition/>\n{0}", _Indent);
				//             </Grid.ColumnDefinitions> 
				_Output.Write ("            </Grid.ColumnDefinitions>\n{0}", _Indent);
				// #foreach (var Entry in Vertical.Entries) 
				foreach  (var Entry in Vertical.Entries) {
					// #switchcast GUISchemaType Entry 
					switch (Entry._Tag ()) {
						// #casecast Action Action 
						case GUISchemaType.Action: {
						  Action Action = (Action) Entry; 
						//             <Button   Grid.Row="#{Row}"  Grid.Column="0" Grid.ColumnSpan="2"  
						_Output.Write ("            <Button   Grid.Row=\"{1}\"  Grid.Column=\"0\" Grid.ColumnSpan=\"2\" \n{0}", _Indent, Row);
						//                       VerticalAlignment="Center" FontSize="20" Width="150" Height="30"  
						_Output.Write ("                      VerticalAlignment=\"Center\" FontSize=\"20\" Width=\"150\" Height=\"30\" \n{0}", _Indent);
						//                   Click="#{Action.XID}" >#{Action.Label}</Button> 
						_Output.Write ("                  Click=\"{1}\" >{2}</Button>\n{0}", _Indent, Action.XID, Action.Label);
						// #% Row ++; 
						
						 Row ++;
						// #casecast Next Next 
						break; }
						case GUISchemaType.Next: {
						  Next Next = (Next) Entry; 
						// #casecast Task Task 
						break; }
						case GUISchemaType.Task: {
						  Task Task = (Task) Entry; 
						// #foreach (var Text in Task.Entries.OfType<Text>()) 
						foreach  (var Text in Task.Entries.OfType<Text>()) {
							//             <TextBlock  Grid.Row="#{Row}" Grid.Column="0" TextWrapping="Wrap" FontSize="20"   
							_Output.Write ("            <TextBlock  Grid.Row=\"{1}\" Grid.Column=\"0\" TextWrapping=\"Wrap\" FontSize=\"20\"  \n{0}", _Indent, Row);
							//                 VerticalAlignment="Center" Margin="20,20,20,20"> 
							_Output.Write ("                VerticalAlignment=\"Center\" Margin=\"20,20,20,20\">\n{0}", _Indent);
							// #foreach (var Line in Text.Data) 
							foreach  (var Line in Text.Data) {
								// 				#{Line} 
								_Output.Write ("				{1}\n{0}", _Indent, Line);
								// #end foreach 
								}
							// 				</TextBlock> 
							_Output.Write ("				</TextBlock>\n{0}", _Indent);
							// #end foreach 
							}
						//             <ProgressBar Grid.Row="#{Row}" Grid.Column="1" Height="30" Width="180" 
						_Output.Write ("            <ProgressBar Grid.Row=\"{1}\" Grid.Column=\"1\" Height=\"30\" Width=\"180\"\n{0}", _Indent, Row);
						//             VerticalAlignment="Center" HorizontalAlignment="Center" Name="Task_#{Task.Target}"/> 
						_Output.Write ("            VerticalAlignment=\"Center\" HorizontalAlignment=\"Center\" Name=\"Task_{1}\"/>\n{0}", _Indent, Task.Target);
						// #% Row ++; 
						
						 Row ++;
						// #casecast Input Input 
						break; }
						case GUISchemaType.Input: {
						  Input Input = (Input) Entry; 
						//             <Label Grid.Row="#{Row}" Grid.Column="0" FontSize="20" 
						_Output.Write ("            <Label Grid.Row=\"{1}\" Grid.Column=\"0\" FontSize=\"20\"\n{0}", _Indent, Row);
						//                    VerticalAlignment="Center">#{Input.Label}</Label> 
						_Output.Write ("                   VerticalAlignment=\"Center\">{1}</Label>\n{0}", _Indent, Input.Label);
						//             <TextBox Grid.Row="#{Row}" Grid.Column="1" FontSize="20" Name="Input_#{Input.Id}" 
						_Output.Write ("            <TextBox Grid.Row=\"{1}\" Grid.Column=\"1\" FontSize=\"20\" Name=\"Input_{2}\"\n{0}", _Indent, Row, Input.Id);
						// 					TextChanged="Changed_#{Input.XID}" Width="400" Height="40"></TextBox> 
						_Output.Write ("					TextChanged=\"Changed_{1}\" Width=\"400\" Height=\"40\"></TextBox>\n{0}", _Indent, Input.XID);
						// #% Row ++; 
						
						 Row ++;
						// #casecast Output Output 
						break; }
						case GUISchemaType.Output: {
						  Output Output = (Output) Entry; 
						//             <Label Grid.Row="#{Row}" Grid.Column="0" FontSize="20" 
						_Output.Write ("            <Label Grid.Row=\"{1}\" Grid.Column=\"0\" FontSize=\"20\"\n{0}", _Indent, Row);
						//                    VerticalAlignment="Center">#{Output.Label}</Label> 
						_Output.Write ("                   VerticalAlignment=\"Center\">{1}</Label>\n{0}", _Indent, Output.Label);
						//             <TextBlock Grid.Row="#{Row}" Grid.Column="1" FontSize="20" Name="Output_#{Output.Id}" 
						_Output.Write ("            <TextBlock Grid.Row=\"{1}\" Grid.Column=\"1\" FontSize=\"20\" Name=\"Output_{2}\"\n{0}", _Indent, Row, Output.Id);
						// 					Width="400" Height="40"></TextBlock> 
						_Output.Write ("					Width=\"400\" Height=\"40\"></TextBlock>\n{0}", _Indent);
						// #% Row ++; 
						
						 Row ++;
						// #end switchcast 
					break; }
						}
					// #end foreach 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				// 		</Grid> 
				_Output.Write ("		</Grid>\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         </StackPanel> 
			_Output.Write ("        </StackPanel>\n{0}", _Indent);
			//     </DockPanel> 
			_Output.Write ("    </DockPanel>\n{0}", _Indent);
			// </Page> 
			_Output.Write ("</Page>\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method GenerateCS Dialog Dialog 
		

		//
		// GenerateCS
		//
		public void GenerateCS (Dialog Dialog) {
			// #% var Wizard = Dialog.Wizard; 
			 var Wizard = Dialog.Wizard;
			// #% var GUI = Dialog.GUI; 
			 var GUI = Dialog.GUI;
			//  
			_Output.Write ("\n{0}", _Indent);
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			// using System.Collections.Generic; 
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			// using System.Linq; 
			_Output.Write ("using System.Linq;\n{0}", _Indent);
			// using System.Text; 
			_Output.Write ("using System.Text;\n{0}", _Indent);
			// using System.Threading.Tasks; 
			_Output.Write ("using System.Threading.Tasks;\n{0}", _Indent);
			// using System.Windows; 
			_Output.Write ("using System.Windows;\n{0}", _Indent);
			// using System.Windows.Controls; 
			_Output.Write ("using System.Windows.Controls;\n{0}", _Indent);
			// using System.Windows.Data; 
			_Output.Write ("using System.Windows.Data;\n{0}", _Indent);
			// using System.Windows.Documents; 
			_Output.Write ("using System.Windows.Documents;\n{0}", _Indent);
			// using System.Windows.Input; 
			_Output.Write ("using System.Windows.Input;\n{0}", _Indent);
			// using System.Windows.Media; 
			_Output.Write ("using System.Windows.Media;\n{0}", _Indent);
			// using System.Windows.Media.Imaging; 
			_Output.Write ("using System.Windows.Media.Imaging;\n{0}", _Indent);
			// using System.Windows.Navigation; 
			_Output.Write ("using System.Windows.Navigation;\n{0}", _Indent);
			// using System.Windows.Shapes; 
			_Output.Write ("using System.Windows.Shapes;\n{0}", _Indent);
			// using System.ComponentModel; 
			_Output.Write ("using System.ComponentModel;\n{0}", _Indent);
			// using System.Threading; 
			_Output.Write ("using System.Threading;\n{0}", _Indent);
			// using Goedel.Trojan; 
			_Output.Write ("using Goedel.Trojan;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// namespace #{GUI.Namespace} { 
			_Output.Write ("namespace {1} {{\n{0}", _Indent, GUI.Namespace);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     public partial class _Data_#{Dialog.Id} :Goedel.Trojan.Data  { 
			_Output.Write ("    public partial class _Data_{1} :Goedel.Trojan.Data  {{\n{0}", _Indent, Dialog.Id);
			// 		public #{Dialog.XID} Dialog; 
			_Output.Write ("		public {1} Dialog;\n{0}", _Indent, Dialog.XID);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		public override Page Page { 
			_Output.Write ("		public override Page Page {{\n{0}", _Indent);
			// 		    get { return Dialog; } 
			_Output.Write ("		    get {{ return Dialog; }}\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		protected #{Wizard.Id} _Data; 
			_Output.Write ("		protected {1} _Data;\n{0}", _Indent, Wizard.Id);
			// 		public #{Wizard.Id} Data { 
			_Output.Write ("		public {1} Data {{\n{0}", _Indent, Wizard.Id);
			// 			get {return _Data;} 
			_Output.Write ("			get {{return _Data;}}\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		public void Refresh () { 
			_Output.Write ("		public void Refresh () {{\n{0}", _Indent);
			// 			if (Dialog != null) { 
			_Output.Write ("			if (Dialog != null) {{\n{0}", _Indent);
			// 				Dialog.Refresh (); 
			_Output.Write ("				Dialog.Refresh ();\n{0}", _Indent);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// //		protected Wizard_#{Wizard.Id}  _Wizard;	 
			_Output.Write ("//		protected Wizard_{1}  _Wizard;	\n{0}", _Indent, Wizard.Id);
			// //		public #{Wizard.XID}  Wizard { 
			_Output.Write ("//		public {1}  Wizard {{\n{0}", _Indent, Wizard.XID);
			// //				get {return _Wizard;} 
			_Output.Write ("//				get {{return _Wizard;}}\n{0}", _Indent);
			// //				} 
			_Output.Write ("//				}}\n{0}", _Indent);
			// //		public #{Wizard.DID}  Data { 
			_Output.Write ("//		public {1}  Data {{\n{0}", _Indent, Wizard.DID);
			// //				get {return Wizard.Data;} 
			_Output.Write ("//				get {{return Wizard.Data;}}\n{0}", _Indent);
			// //				} 
			_Output.Write ("//				}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		// Input backing variables 
			_Output.Write ("		// Input backing variables\n{0}", _Indent);
			// #foreach (var Input in Dialog.Inputs) 
			foreach  (var Input in Dialog.Inputs) {
				// 		string _#{Input.XID}; 
				_Output.Write ("		string _{1};\n{0}", _Indent, Input.XID);
				// 		public string #{Input.XID} { 
				_Output.Write ("		public string {1} {{\n{0}", _Indent, Input.XID);
				//             get { return _#{Input.XID}; } 
				_Output.Write ("            get {{ return _{1}; }}\n{0}", _Indent, Input.XID);
				//             set { _#{Input.XID} = value;  Refresh (); } 
				_Output.Write ("            set {{ _{1} = value;  Refresh (); }}\n{0}", _Indent, Input.XID);
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		// Output backing variables 
			_Output.Write ("		// Output backing variables\n{0}", _Indent);
			// #foreach (var Output in Dialog.Outputs) 
			foreach  (var Output in Dialog.Outputs) {
				// 		string _#{Output.XID}; 
				_Output.Write ("		string _{1};\n{0}", _Indent, Output.XID);
				// 		public string #{Output.XID} { 
				_Output.Write ("		public string {1} {{\n{0}", _Indent, Output.XID);
				//             get { return _#{Output.XID}; } 
				_Output.Write ("            get {{ return _{1}; }}\n{0}", _Indent, Output.XID);
				//             set { _#{Output.XID} = value;   Refresh (); #! 
				_Output.Write ("            set {{ _{1} = value;   Refresh (); ", _Indent, Output.XID);
				// #if (Dialog.Tasks.Count > 0) 
				if (  (Dialog.Tasks.Count > 0) ) {
					// 				 
					_Output.Write ("				\n{0}", _Indent);
					// 				//if (Dialog != null) { Dialog.UpdateProgress(); }  
					_Output.Write ("				//if (Dialog != null) {{ Dialog.UpdateProgress(); }} \n{0}", _Indent);
					// #end if 
					}
				// } 
				_Output.Write ("}}\n{0}", _Indent);
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if (Dialog.Tasks.Count > 0) 
			if (  (Dialog.Tasks.Count > 0) ) {
				// #foreach (var Task in Dialog.Tasks) 
				foreach  (var Task in Dialog.Tasks) {
					// 		public int Completion_#{Task.Target} = 0; 
					_Output.Write ("		public int Completion_{1} = 0;\n{0}", _Indent, Task.Target);
					// #end foreach 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				// #foreach (var Task in Dialog.Tasks) 
				foreach  (var Task in Dialog.Tasks) {
					// 		public virtual void Do_#{Task.Target} () { 
					_Output.Write ("		public virtual void Do_{1} () {{\n{0}", _Indent, Task.Target);
					//             Completion_#{Task.Target} = -1; 
					_Output.Write ("            Completion_{1} = -1;\n{0}", _Indent, Task.Target);
					// 			Dialog.UpdateProgress (); 
					_Output.Write ("			Dialog.UpdateProgress ();\n{0}", _Indent);
					// 			#{Task.Target} (); 
					_Output.Write ("			{1} ();\n{0}", _Indent, Task.Target);
					//             Completion_#{Task.Target} = 100; 
					_Output.Write ("            Completion_{1} = 100;\n{0}", _Indent, Task.Target);
					// 			Dialog.UpdateProgress (); 
					_Output.Write ("			Dialog.UpdateProgress ();\n{0}", _Indent);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		public virtual void #{Task.Target} () { 
					_Output.Write ("		public virtual void {1} () {{\n{0}", _Indent, Task.Target);
					//             Thread.Sleep(2000); 
					_Output.Write ("            Thread.Sleep(2000);\n{0}", _Indent);
					//             Completion_#{Task.Target} = 100; 
					_Output.Write ("            Completion_{1} = 100;\n{0}", _Indent, Task.Target);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					// #end foreach 
					}
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Action in Dialog.Actions) 
			foreach  (var Action in Dialog.Actions) {
				// 		/// <summary> 
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				// 		/// Here goes the action to be overriden 
				_Output.Write ("		/// Here goes the action to be overriden\n{0}", _Indent);
				// 		/// </summary> 
				_Output.Write ("		/// </summary>\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// 		public virtual bool #{Action.Id} () { 
				_Output.Write ("		public virtual bool {1} () {{\n{0}", _Indent, Action.Id);
				// 			return true; 
				_Output.Write ("			return true;\n{0}", _Indent);
				// 			} 
				_Output.Write ("			}}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		} 
			_Output.Write ("		}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     public partial class #{Dialog.Id} : _Data_#{Dialog.Id} { 
			_Output.Write ("    public partial class {1} : _Data_{2} {{\n{0}", _Indent, Dialog.Id, Dialog.Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		 
			_Output.Write ("		\n{0}", _Indent);
			// 		public #{Dialog.Id} (#{Wizard.Id}  Data) { 
			_Output.Write ("		public {1} ({2}  Data) {{\n{0}", _Indent, Dialog.Id, Wizard.Id);
			// 			_Data = Data; 
			_Output.Write ("			_Data = Data;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			// NB call to the initializer before we creaate the dialog so the 
			_Output.Write ("			// NB call to the initializer before we creaate the dialog so the\n{0}", _Indent);
			// 			// dialog can display the initialized data. 
			_Output.Write ("			// dialog can display the initialized data.\n{0}", _Indent);
			// 			Initialize (); 
			_Output.Write ("			Initialize ();\n{0}", _Indent);
			// 			this.Dialog = new #{Dialog.XID} (this); 
			_Output.Write ("			this.Dialog = new {1} (this);\n{0}", _Indent, Dialog.XID);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			// 		} 
			_Output.Write ("		}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     /// <summary> 
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			// 	/// This is the code behind for the XAML generated class. 
			_Output.Write ("	/// This is the code behind for the XAML generated class.\n{0}", _Indent);
			//     /// </summary> 
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			//     public partial class Dialog_#{Dialog.Id} : Page { 
			_Output.Write ("    public partial class Dialog_{1} : Page {{\n{0}", _Indent, Dialog.Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		public #{Dialog.Id}  Data; 
			_Output.Write ("		public {1}  Data;\n{0}", _Indent, Dialog.Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if (Dialog.Tasks.Count > 0) 
			if (  (Dialog.Tasks.Count > 0) ) {
				// 		public BackgroundWorker BackgroundWorker; 
				_Output.Write ("		public BackgroundWorker BackgroundWorker;\n{0}", _Indent);
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		public Dialog_#{Dialog.Id} (#{Dialog.Id} Data) { 
			_Output.Write ("		public Dialog_{1} ({2} Data) {{\n{0}", _Indent, Dialog.Id, Dialog.Id);
			// 			InitializeComponent(); 
			_Output.Write ("			InitializeComponent();\n{0}", _Indent);
			// 			this.Data = Data; 
			_Output.Write ("			this.Data = Data;\n{0}", _Indent);
			// 			Refresh (); 
			_Output.Write ("			Refresh ();\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if (Dialog.Tasks.Count > 0) 
			if (  (Dialog.Tasks.Count > 0) ) {
				//             BackgroundWorker = new BackgroundWorker(); 
				_Output.Write ("            BackgroundWorker = new BackgroundWorker();\n{0}", _Indent);
				//             BackgroundWorker.WorkerReportsProgress = true; 
				_Output.Write ("            BackgroundWorker.WorkerReportsProgress = true;\n{0}", _Indent);
				//             BackgroundWorker.DoWork += new DoWorkEventHandler(DoWork); 
				_Output.Write ("            BackgroundWorker.DoWork += new DoWorkEventHandler(DoWork);\n{0}", _Indent);
				//             BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted); 
				_Output.Write ("            BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);\n{0}", _Indent);
				//             BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged); 
				_Output.Write ("            BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);\n{0}", _Indent);
				//             BackgroundWorker.RunWorkerAsync(); 
				_Output.Write ("            BackgroundWorker.RunWorkerAsync();\n{0}", _Indent);
				// #end if 
				}
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if (Dialog.Tasks.Count > 0) 
			if (  (Dialog.Tasks.Count > 0) ) {
				//         // Should probably move this to the Data class so that it can be inherited 
				_Output.Write ("        // Should probably move this to the Data class so that it can be inherited\n{0}", _Indent);
				//         public void DoWork(object sender, DoWorkEventArgs e) { 
				_Output.Write ("        public void DoWork(object sender, DoWorkEventArgs e) {{\n{0}", _Indent);
				// #foreach (var Task in Dialog.Tasks) 
				foreach  (var Task in Dialog.Tasks) {
					// 			Data.Do_#{Task.Target} (); 
					_Output.Write ("			Data.Do_{1} ();\n{0}", _Indent, Task.Target);
					// #end foreach 
					}
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         public void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { 
				_Output.Write ("        public void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {{\n{0}", _Indent);
				// #foreach (var Next in Dialog.Nexts) 
				foreach  (var Next in Dialog.Nexts) {
					// 			Data.Data.Navigate (Data.Data.Data_#{Next.Target}); 
					_Output.Write ("			Data.Data.Navigate (Data.Data.Data_{1});\n{0}", _Indent, Next.Target);
					// #end foreach 
					}
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         public void ProgressChanged(object sender, ProgressChangedEventArgs e) { 
				_Output.Write ("        public void ProgressChanged(object sender, ProgressChangedEventArgs e) {{\n{0}", _Indent);
				// #foreach (var Task in Dialog.Tasks) 
				foreach  (var Task in Dialog.Tasks) {
					// 			if (Data.Completion_#{Task.Target} >= 0) { 
					_Output.Write ("			if (Data.Completion_{1} >= 0) {{\n{0}", _Indent, Task.Target);
					// 				#{Task.XID}.Value = Data.Completion_#{Task.Target}; 
					_Output.Write ("				{1}.Value = Data.Completion_{2};\n{0}", _Indent, Task.XID, Task.Target);
					// 				#{Task.XID}.IsIndeterminate = false; 
					_Output.Write ("				{1}.IsIndeterminate = false;\n{0}", _Indent, Task.XID);
					// 				} 
					_Output.Write ("				}}\n{0}", _Indent);
					// 			else { 
					_Output.Write ("			else {{\n{0}", _Indent);
					// 				#{Task.XID}.IsIndeterminate = true; 
					_Output.Write ("				{1}.IsIndeterminate = true;\n{0}", _Indent, Task.XID);
					// 				} 
					_Output.Write ("				}}\n{0}", _Indent);
					// #end foreach 
					}
				// 			Refresh (); 
				_Output.Write ("			Refresh ();\n{0}", _Indent);
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// 		public void UpdateProgress () { 
				_Output.Write ("		public void UpdateProgress () {{\n{0}", _Indent);
				// 			BackgroundWorker.ReportProgress(100); 
				_Output.Write ("			BackgroundWorker.ReportProgress(100);\n{0}", _Indent);
				// 			}  
				_Output.Write ("			}} \n{0}", _Indent);
				// #end if 
				}
			// 		public void Refresh () { 
			_Output.Write ("		public void Refresh () {{\n{0}", _Indent);
			// #foreach (var Input in Dialog.Inputs) 
			foreach  (var Input in Dialog.Inputs) {
				// 			#{Input.XID}.Text  = Data.#{Input.XID}; 
				_Output.Write ("			{1}.Text  = Data.{2};\n{0}", _Indent, Input.XID, Input.XID);
				// #end foreach 
				}
			// #foreach (var Output in Dialog.Outputs) 
			foreach  (var Output in Dialog.Outputs) {
				// 			#{Output.XID}.Text  = Data.#{Output.XID}; 
				_Output.Write ("			{1}.Text  = Data.{2};\n{0}", _Indent, Output.XID, Output.XID);
				// #end foreach 
				}
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Action in Dialog.Actions) 
			foreach  (var Action in Dialog.Actions) {
				// #call GenerateCS Action 
				GenerateCS (Action);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Task in Dialog.Tasks) 
			foreach  (var Task in Dialog.Tasks) {
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Input in Dialog.Inputs) 
			foreach  (var Input in Dialog.Inputs) {
				// 		private void Changed_#{Input.XID} (object sender, TextChangedEventArgs e) { 
				_Output.Write ("		private void Changed_{1} (object sender, TextChangedEventArgs e) {{\n{0}", _Indent, Input.XID);
				// 			Data.#{Input.XID} = #{Input.XID}.Text; 
				_Output.Write ("			Data.{1} = {2}.Text;\n{0}", _Indent, Input.XID, Input.XID);
				// 			} 
				_Output.Write ("			}}\n{0}", _Indent);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		} 
			_Output.Write ("		}}\n{0}", _Indent);
			// 	} 
			_Output.Write ("	}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method GenerateCS Action Action 
		

		//
		// GenerateCS
		//
		public void GenerateCS (Action Action) {
			//         private void Action_#{Action.Id}(object sender, RoutedEventArgs e) { 
			_Output.Write ("        private void Action_{1}(object sender, RoutedEventArgs e) {{\n{0}", _Indent, Action.Id);
			// 			var Result = Data.#{Action.Id} (); 
			_Output.Write ("			var Result = Data.{1} ();\n{0}", _Indent, Action.Id);
			// 			if (Result) { 
			_Output.Write ("			if (Result) {{\n{0}", _Indent);
			// 				Data.Data.Navigate (Data.Data.Data_#{Action.Target}); 
			_Output.Write ("				Data.Data.Navigate (Data.Data.Data_{1});\n{0}", _Indent, Action.Target);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method GenerateXAML Wizard Wizard 
		

		//
		// GenerateXAML
		//
		public void GenerateXAML (Wizard Wizard) {
			// <Window x:Class="#{Wizard.GUI.Namespace}.Wizard_#{Wizard.Id}" 
			_Output.Write ("<Window x:Class=\"{1}.Wizard_{2}\"\n{0}", _Indent, Wizard.GUI.Namespace, Wizard.Id);
			//         xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
			_Output.Write ("        xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\n{0}", _Indent);
			//         xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" 
			_Output.Write ("        xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\n{0}", _Indent);
			//         Title="#{Wizard.Tag}" 
			_Output.Write ("        Title=\"{1}\"\n{0}", _Indent, Wizard.Tag);
			// 		Width="800" Height="600"> 
			_Output.Write ("		Width=\"800\" Height=\"600\">\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     <Frame Name="Main" Width="800" Height="600"/> 
			_Output.Write ("    <Frame Name=\"Main\" Width=\"800\" Height=\"600\"/>\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// </Window> 
			_Output.Write ("</Window>\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method GenerateCS Wizard Wizard 
		

		//
		// GenerateCS
		//
		public void GenerateCS (Wizard Wizard) {
			// #% var GUI = Wizard.GUI; 
			 var GUI = Wizard.GUI;
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			// using System.Collections.Generic; 
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			// using System.Linq; 
			_Output.Write ("using System.Linq;\n{0}", _Indent);
			// using System.Text; 
			_Output.Write ("using System.Text;\n{0}", _Indent);
			// using System.Threading.Tasks; 
			_Output.Write ("using System.Threading.Tasks;\n{0}", _Indent);
			// using System.Windows; 
			_Output.Write ("using System.Windows;\n{0}", _Indent);
			// using System.Windows.Controls; 
			_Output.Write ("using System.Windows.Controls;\n{0}", _Indent);
			// using System.Windows.Data; 
			_Output.Write ("using System.Windows.Data;\n{0}", _Indent);
			// using System.Windows.Documents; 
			_Output.Write ("using System.Windows.Documents;\n{0}", _Indent);
			// using System.Windows.Input; 
			_Output.Write ("using System.Windows.Input;\n{0}", _Indent);
			// using System.Windows.Media; 
			_Output.Write ("using System.Windows.Media;\n{0}", _Indent);
			// using System.Windows.Media.Imaging; 
			_Output.Write ("using System.Windows.Media.Imaging;\n{0}", _Indent);
			// using System.Windows.Shapes; 
			_Output.Write ("using System.Windows.Shapes;\n{0}", _Indent);
			// using System.Threading; 
			_Output.Write ("using System.Threading;\n{0}", _Indent);
			// using System.ComponentModel; 
			_Output.Write ("using System.ComponentModel;\n{0}", _Indent);
			// using Goedel.Trojan; 
			_Output.Write ("using Goedel.Trojan;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// namespace #{GUI.Namespace} { 
			_Output.Write ("namespace {1} {{\n{0}", _Indent, GUI.Namespace);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     public partial class #{Wizard.XID} : Window { 
			_Output.Write ("    public partial class {1} : Window {{\n{0}", _Indent, Wizard.XID);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         public #{Wizard.Id} Data; 
			_Output.Write ("        public {1} Data;\n{0}", _Indent, Wizard.Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         public #{Wizard.XID}() { 
			_Output.Write ("        public {1}() {{\n{0}", _Indent, Wizard.XID);
			//             InitializeComponent(); 
			_Output.Write ("            InitializeComponent();\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             Data = new #{Wizard.Id}(this); 
			_Output.Write ("            Data = new {1}(this);\n{0}", _Indent, Wizard.Id);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         } 
			_Output.Write ("        }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     public partial class #{Wizard.Id} : Goedel.Trojan.Data { 
			_Output.Write ("    public partial class {1} : Goedel.Trojan.Data {{\n{0}", _Indent, Wizard.Id);
			//         public #{Wizard.XID} Wizard; 
			_Output.Write ("        public {1} Wizard;\n{0}", _Indent, Wizard.XID);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #% Dialog StartDialog = null; 
			 Dialog StartDialog = null;
			// #foreach (var Dialog in Wizard.Entries.OfType<Dialog>()) 
			foreach  (var Dialog in Wizard.Entries.OfType<Dialog>()) {
				// #% StartDialog = StartDialog != null ? StartDialog : Dialog;  
				 StartDialog = StartDialog != null ? StartDialog : Dialog; 
				// 		#{Dialog.Id} _Data_#{Dialog.Id} = null; 
				_Output.Write ("		{1} _Data_{2} = null;\n{0}", _Indent, Dialog.Id, Dialog.Id);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Dialog in Wizard.Entries.OfType<Dialog>()) 
			foreach  (var Dialog in Wizard.Entries.OfType<Dialog>()) {
				// 		public #{Dialog.Id} Data_#{Dialog.Id} { 
				_Output.Write ("		public {1} Data_{2} {{\n{0}", _Indent, Dialog.Id, Dialog.Id);
				// 			get { _Data_#{Dialog.Id} = _Data_#{Dialog.Id} != null ? _Data_#{Dialog.Id} : new #{Dialog.Id} (this); 
				_Output.Write ("			get {{ _Data_{1} = _Data_{2} != null ? _Data_{3} : new {4} (this);\n{0}", _Indent, Dialog.Id, Dialog.Id, Dialog.Id, Dialog.Id);
				// 			return _Data_#{Dialog.Id}; } } 
				_Output.Write ("			return _Data_{1}; }} }}\n{0}", _Indent, Dialog.Id);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Dialog in Wizard.Entries.OfType<Dialog>()) 
			foreach  (var Dialog in Wizard.Entries.OfType<Dialog>()) {
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		public #{Wizard.Id} (#{Wizard.XID} Wizard) { 
			_Output.Write ("		public {1} ({2} Wizard) {{\n{0}", _Indent, Wizard.Id, Wizard.XID);
			// 			this.Wizard = Wizard; 
			_Output.Write ("			this.Wizard = Wizard;\n{0}", _Indent);
			// 			Initialize (); 
			_Output.Write ("			Initialize ();\n{0}", _Indent);
			// #if (StartDialog!= null)  
			if (  (StartDialog!= null)  ) {
				// 			if (CurrentDialog == null) { 
				_Output.Write ("			if (CurrentDialog == null) {{\n{0}", _Indent);
				// 				Navigate (Data_#{StartDialog.Id}); 
				_Output.Write ("				Navigate (Data_{1});\n{0}", _Indent, StartDialog.Id);
				// 				} 
				_Output.Write ("				}}\n{0}", _Indent);
				// #end if 
				}
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		/// <summary> 
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			// 		/// The currently active dialog 
			_Output.Write ("		/// The currently active dialog\n{0}", _Indent);
			// 		/// </summary> 
			_Output.Write ("		/// </summary>\n{0}", _Indent);
			// 		public Goedel.Trojan.Data CurrentDialog = null ; 
			_Output.Write ("		public Goedel.Trojan.Data CurrentDialog = null ;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		/// <summary> 
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			// 		/// Navigate to a new dialog. 
			_Output.Write ("		/// Navigate to a new dialog.\n{0}", _Indent);
			// 		/// </summary> 
			_Output.Write ("		/// </summary>\n{0}", _Indent);
			// 		public void Navigate (Goedel.Trojan.Data Dialog) { 
			_Output.Write ("		public void Navigate (Goedel.Trojan.Data Dialog) {{\n{0}", _Indent);
			// 			if (CurrentDialog != null) { 
			_Output.Write ("			if (CurrentDialog != null) {{\n{0}", _Indent);
			// 				CurrentDialog.Exit (); 
			_Output.Write ("				CurrentDialog.Exit ();\n{0}", _Indent);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// 			CurrentDialog = Dialog; 
			_Output.Write ("			CurrentDialog = Dialog;\n{0}", _Indent);
			// 			CurrentDialog.Enter (); 
			_Output.Write ("			CurrentDialog.Enter ();\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			Wizard.Main.Navigate (Dialog.Page); 
			_Output.Write ("			Wizard.Main.Navigate (Dialog.Page);\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		} 
			_Output.Write ("		}}\n{0}", _Indent);
			// 	} 
			_Output.Write ("	}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		// #end pclass 
		}
	}
