using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace RimworldApparelBodyTex_V1
{
    class ThreadWorker_GenThingDefXML
    {
    }

    //GenThingDefXML

    public partial class Form1 : Form
    {
        //GenThingDefXML
        bool IsThreadRun_GenThingDefXML = false;

        private void startThread_GenThingDefXML()
        {


            if (IsThreadRun_GenThingDefXML)
            {
                Log("GenThingDefXML Already Running");
                MessageBox.Show(btn_GenThingDefXML.Text + " is already running.");
                return;
            }

            //setup setting first
            //MySettings[0].BaseSourceTexDir = textBox_sourceTex.Text;
            //MySettings[0].TexDestinationPath = textBox_TexDestinationPath.Text;

            string DirSrc = "";
            this.InvokeEx(f=> DirSrc = f.textBox_tab6_sourceData.Text);
            string DirDst = "";
            this.InvokeEx(f => DirDst = f.textBox_tab6_outputData.Text);

            //check up
            if (!Directory.Exists(DirSrc))
            {
                string s = "Source dir not exists";
                Log(s);
                MessageBox.Show(s);
                return;
            }

            if (!Directory.Exists(DirDst))
            {
                string s = "Destination dir not exists";
                Log(s);
                MessageBox.Show(s);
                return;
            }

            //start the thread
            var th = new Thread(Thread_GenThingDefXML);
            th.IsBackground = true;
            th.Start();
            //Thread.Sleep(1000);
            Log("Main thread ({0}) ",
                              Thread.CurrentThread.ManagedThreadId);
        }

        private void Thread_GenThingDefXML()
        {
            IsThreadRun_GenThingDefXML = true;
            //Console.WriteLine("Thread ({0}) running.",
            //                  Thread.CurrentThread.ManagedThreadId);
            Log("Thread ({0}) running.",
                              Thread.CurrentThread.ManagedThreadId);

            //start run timer
            DateTime beginTime = DateTime.Now;

            //labeling in
            string defaultBtnLabel = "";
            this.InvokeEx(f => defaultBtnLabel = f.btn_GenThingDefXML.Text);
            this.InvokeEx(f => f.btn_GenThingDefXML.Text = "... In Progress ...");


            //start
            //=================================================================================1
            Thread_DoGenThingDefXML();

            //=================================================================================1
            //end

            //labeling out
            this.InvokeEx(f => f.btn_GenThingDefXML.Text = defaultBtnLabel);

            //end timer
            DateTime endTime = DateTime.Now;
            TimeSpan runTime = endTime - beginTime;

            Log("Thread ({1}) Done. Runtime {0}.", runTime, Thread.CurrentThread.ManagedThreadId);

            IsThreadRun_GenThingDefXML = false;
        }


        string DirDst_tab6 = "";

        private void Thread_DoGenThingDefXML()
        {
            //do stuff
            string DirSrc = "";
            this.InvokeEx(f => DirSrc = f.textBox_tab6_sourceData.Text);
            string DirDst = "";
            this.InvokeEx(f => DirDst = f.textBox_tab6_outputData.Text);
            DirDst_tab6 = DirDst;
            IList<string> List_XMLFilePaths = new List<string>();
            DirectorySearchAllFiles_WithRuleset(List_XMLFilePaths, DirSrc, FuncRuleset_ValidXMLFile);

            //xml holder
            XmlDocument XMLdoc_ThingDef_Merge = new XmlDocument();
            

            //list normal thingDef and abstract thingDef
            IList<ThingDefObject> List_DefName_ThingDef = new List<ThingDefObject>();
            IList<ThingDefObject> List_AbstractThingDef = new List<ThingDefObject>();

            XMLdoc_ThingDef_Merge = DoListDefName_ThingDef(List_XMLFilePaths, List_DefName_ThingDef, List_AbstractThingDef);


            //save xml
            string newXMLFilePath = Path.Combine(DirDst, "ThinDefMerge.xml");
            XMLdoc_ThingDef_Merge.Save(newXMLFilePath);

        }

        private XmlDocument DoListDefName_ThingDef(IList<string> List_XMLFilePaths, IList<ThingDefObject> List_DefName_ThingDef, IList<ThingDefObject> List_AbstractThingDef)
        {
            //create root thing
            XmlDocument XDoc_thing = new XmlDocument();// = XMLdoc_ThingDef_Merge.ToXDocument();            
            XmlElement root = XDoc_thing.CreateElement("Def");
            XDoc_thing.AppendChild(root);

            //create root abstract
            XmlDocument XDoc_abstract = new XmlDocument();// = XMLdoc_ThingDef_Merge.ToXDocument();            
            XmlElement root_abstract = XDoc_abstract.CreateElement("Def");
            XDoc_abstract.AppendChild(root_abstract);

            //create root merge
            XmlDocument XDoc_merge = new XmlDocument();// = XMLdoc_ThingDef_Merge.ToXDocument();            
            XmlElement root_merge = XDoc_merge.CreateElement("Def");
            XDoc_merge.AppendChild(root_merge);




            //root.SetAttribute("name", "value");
            //XmlElement child = doc.CreateElement("child");
            //child.InnerText = "text node";
            //root.AppendChild(child);



            int i = 0;
            foreach (string xmlfilepath in List_XMLFilePaths)
            {
                i++;
                Log("xmlfilepath = {0}", xmlfilepath);
                string XMLstring = File.ReadAllText(xmlfilepath);

                XmlDocument XMLdoc = new XmlDocument();
                XMLdoc.LoadXml(XMLstring);

                XmlNode RootNode = XMLdoc.SelectSingleNode("/Defs"); //NOTE: case sensitive

                bool bValid = true;

                if (RootNode is null)
                {
                    bValid = false;
                    Log("<ERROR> Fail to validity Apparel_Various xml. Root is null. " + xmlfilepath);
                    //return XDoc1;
                }


                //get ThingDefNodes
                XmlNodeList ThingDefNodes = bValid? RootNode.SelectNodes("ThingDef"):null;

                if (ThingDefNodes is null)
                {
                    bValid = false;
                    Log("<ERROR> Fail to validity Apparel_Various xml. No ThingDef. " + xmlfilepath);
                    //return XDoc1;
                }

                //get all ThingDef and split into 2, a thing and abstract
                if ((RootNode != null) & (ThingDefNodes != null))
                {
                    foreach (XmlNode ThingDefNode in ThingDefNodes)
                    {
                        XmlAttribute Abstract = ThingDefNode.Attributes["Abstract"];
                        bool IsAbstract = false;
                        if (Abstract != null)
                        {
                            IsAbstract = Convert.ToBoolean(Abstract.Value);
                        }

                        if (IsAbstract)
                        {
                            Log("ThingDefNode abstract, {0}", ThingDefNode.Attributes["Name"].Value);
                            //
                            //XmlNode importNode = XDoc_abstract.OwnerDocument.ImportNode(ThingDefNode, true);
                            XmlNode nodeCopy = XDoc_abstract.ImportNode(ThingDefNode, true);
                            XDoc_abstract.FirstChild.AppendChild(nodeCopy);



                            //XmlDocument targetDoc = nodeToWriteOn.OwnerDocument; 
                            //XmlNode nodeToAdd = targetDoc.ImportNode(node, true); 
                            //nodeToWriteOn.AppendChild(nodeToAdd);

                        }
                        else
                        {
                            
                            XmlNode DefNameNode = ThingDefNode.SelectSingleNode("defName");
                            if (DefNameNode != null)
                            {
                                Log("ThingDefNode normal, {0}", DefNameNode.InnerText);
                                //XmlNode importNode = XDoc_abstract.OwnerDocument.ImportNode(ThingDefNode, true);
                                //XDoc1.AppendChild(importNode);



                                //XDoc1.AppendChild(ThingDefNode);
                                //XDoc1.ImportNode(ThingDefNode,true);


                                XmlNode nodeCopy = XDoc_thing.ImportNode(ThingDefNode, true);
                                XDoc_thing.FirstChild.AppendChild(nodeCopy);


                                //string sDefName = DefNameNode.InnerText;
                                //List_DefName_ThingDef.Add(sDefName);
                            }
                            else
                            {
                                Log("ThingDefNode normal, but DefName Node contain no innerText, skip append node to thing list");
                            }
                        }


                    }
                }
                

                

            }



            Log("save");
            //save xml
            string newXMLFilePath = Path.Combine(DirDst_tab6, "ThinDefMerge1_normal.xml");
            XDoc_thing.Save(newXMLFilePath);
            //save xml
             newXMLFilePath = Path.Combine(DirDst_tab6, "ThinDefMerge1_abstract.xml");
            XDoc_abstract.Save(newXMLFilePath);


            //start merge
            XmlNodeList ThingDefNodes_src = XDoc_thing.FirstChild.SelectNodes("ThingDef");

            if (ThingDefNodes_src is null)
            {
                Log("<ERROR> Fail to validity ThingDefNodes_src. No ThingDef. ");
                //return XDoc1;
            }
            foreach (XmlNode BaseThingDefNode in ThingDefNodes_src)
            {
                //
                Log("===========================================================");
                XmlNode defNameNode = BaseThingDefNode.SelectSingleNode("defName");
                string defNameString = "";
                if (defNameNode != null)
                {
                    defNameString = defNameNode.InnerText;
                }
                
                XmlAttribute ParentNameAttr = BaseThingDefNode.Attributes["ParentName"];
                if (ParentNameAttr != null)
                {
                    Log("{0} using parent", defNameString);
                    string parentName = ParentNameAttr.Value;
                    XmlNode nodeCopy = XDoc_merge.ImportNode(XMLNodeSearchParentToMerge(XDoc_thing, BaseThingDefNode, XDoc_abstract.FirstChild, XDoc_thing.FirstChild, parentName, 0), true);
                    XDoc_merge.FirstChild.AppendChild(nodeCopy);
                } else
                {
                    Log("{0} not using parent", defNameString);
                    XmlNode nodeCopy = XDoc_merge.ImportNode(BaseThingDefNode, true);
                    XDoc_merge.FirstChild.AppendChild(nodeCopy);
                }
               
            }



            return XDoc_merge;
        }


        private XmlNode XMLNodeSearchParentToMerge(XmlDocument XDoc_merge, XmlNode BaseThingDefNode, XmlNode DefsNodeToSearch_abstract, XmlNode DefsNodeToSearch_thing, string _ParentName, int levelParent)
        {
            levelParent++;
            XmlNodeList ThingDefNodes = DefsNodeToSearch_abstract.ChildNodes;
            foreach (XmlNode ThingDefNode in ThingDefNodes)
            {
                //
                XmlAttribute NameAttr = ThingDefNode.Attributes["Name"];
                XmlAttribute ParentNameAttr = ThingDefNode.Attributes["ParentName"];
                if (NameAttr != null)
                {
                    string slName = NameAttr.Value.ToLower();
                    if (slName== _ParentName.ToLower())
                    {
                        Log("Found parentName {0}, level {1}", NameAttr.Value, levelParent);

                        //merge first
                        Log("merging {0} to {1}", NameAttr.Value, "child node");
                        XmlNode nodeCopy = MergeNodeBToNodeA(BaseThingDefNode, ThingDefNode, XDoc_merge);

                        if (ParentNameAttr != null)
                        {
                            
                            string parentName = ParentNameAttr.Value;
                            Log("next parentName to search = {0}", parentName);

                            

                            return XMLNodeSearchParentToMerge(XDoc_merge, nodeCopy, DefsNodeToSearch_abstract, DefsNodeToSearch_thing, parentName, levelParent);
                        }
                        else
                        {
                            Log("no parent left");
                            return nodeCopy;
                        }

                    }
                }


            }

            //Log("return DefsNodeToSearch");
            //return DefsNodeToSearch;

            Log("Cant find next parent {0} from abstract, switch to thing", _ParentName);



            //=======================================================================================thing
            XmlNodeList ThingDefNodess = DefsNodeToSearch_thing.ChildNodes;
            foreach (XmlNode ThingDefNode in ThingDefNodess)
            {
                //
                XmlAttribute NameAttr = ThingDefNode.Attributes["Name"];
                XmlAttribute ParentNameAttr = ThingDefNode.Attributes["ParentName"];
                if (NameAttr != null)
                {
                    string slName = NameAttr.Value.ToLower();
                    if (slName == _ParentName.ToLower())
                    {
                        Log("Found parentName {0}, level {1}", NameAttr.Value, levelParent);

                        //merge first
                        Log("merging {0} to {1}", NameAttr.Value, "child node");
                        XmlNode nodeCopy = MergeNodeBToNodeA(BaseThingDefNode, ThingDefNode, XDoc_merge);

                        if (ParentNameAttr != null)
                        {

                            string parentName = ParentNameAttr.Value;
                            Log("next parentName to search = {0}", parentName);



                            return XMLNodeSearchParentToMerge(XDoc_merge, nodeCopy, DefsNodeToSearch_abstract, DefsNodeToSearch_thing, parentName, levelParent);
                        }
                        else
                        {
                            Log("ok, no parent left");
                            return nodeCopy;
                        }

                    }
                }


            }
            //=======================================================================================thing

            Log("OMG STILL Cant find next parent {0} from thing, nothing todo, ParentName = ", _ParentName);

            return BaseThingDefNode;
        }

        //XML helper
        //XmlDocument MergeDocs(string SourceA, string SourceB)
        XmlDocument MergeDocs(XmlDocument docA, XmlDocument docB)
        {

            //XmlDocument docA = new XmlDocument();
            //XmlDocument docB = new XmlDocument();
            XmlDocument merged = new XmlDocument();

            //docA.LoadXml(SourceA);
            //docB.LoadXml(SourceB);

            var childsFromA = docA.ChildNodes.Cast<XmlNode>();
            var childsFromB = docB.ChildNodes.Cast<XmlNode>();

            var uniquesFromA = childsFromA.Where(ch => childsFromB.Where(chb => chb.Name == ch.Name).Count() == 0);
            var uniquesFromB = childsFromB.Where(ch => childsFromA.Where(chb => chb.Name == ch.Name).Count() == 0);

            foreach (var unique in uniquesFromA)
                merged.AppendChild(DeepCloneToDoc(unique, merged));

            foreach (var unique in uniquesFromA)
                merged.AppendChild(DeepCloneToDoc(unique, merged));

            var Duplicates = from chA in childsFromA
                             from chB in childsFromB
                             where chA.Name == chB.Name
                             select new { A = chA, B = chB };

            foreach (var grp in Duplicates)
                merged.AppendChild(MergeNodes(grp.A, grp.B, merged));

            return merged;

        }


        XmlNode MergeNodeBToNodeA(XmlNode NodeA, XmlNode NodeB, XmlDocument TargetDoc)
        {
            //var merged = NodeA;
            if (NodeB.ChildNodes.Count > 0)
            {
                int iAdd = 0;
                //search same node
                foreach (XmlNode NodeB1 in NodeB.ChildNodes)
                {

                    if (NodesContainName(NodeB1.Name, NodeA.ChildNodes))
                    {
                        //skip that already in A?

                        //not skip
                        XmlNode NodeA1 = GetNodeByName(NodeB1.Name, NodeA.ChildNodes);

                        XmlNode nodeCopy = MergeNodeBToNodeA(NodeA1, NodeB1, TargetDoc);
                        NodeA.AppendChild(nodeCopy);

                    }
                    else
                    {
                        iAdd++;
                        //copy node that not exist
                        //XmlNode nodeCopy = TargetDoc.ImportNode(NodeB1,true);
                        //TargetDoc.FirstChild.AppendChild(nodeCopy);


                        //string NodeName = NodeB1.Name;
                        //string NodeInnerText = NodeB1.InnerText;
                        //XmlElement ele = TargetDoc.CreateElement(NodeName);
                        //ele.InnerText = NodeInnerText;
                        //NodeA.AppendChild(ele);

                        XmlNode nodeCopy = TargetDoc.ImportNode(NodeB1, true);
                        NodeA.AppendChild(nodeCopy);

                    }
                    

                }

                if (iAdd == 0)
                {
                    Log("Trying to merge but all childs NodeB({0}) already in NodeA({1}), nothing to merge", NodeB.Name, NodeA.Name);
                }

            }
            else
            {
                Log("Can not merge because NodeB({0}) contain no childs to merge with NodeA({1})", NodeB.Name, NodeA.Name);
            }
           

            return NodeA;
        }

        private XmlNode GetNodeByName(string NodeName, XmlNodeList Nodes)
        {
            foreach(XmlNode node in Nodes)
            {
                if (node.Name.ToLower() == NodeName.ToLower())
                {
                    return node;
                }
            }

            return null;
        }

        private bool NodesContainName(string NodeName, XmlNodeList Nodes)
        {
            foreach (XmlNode node in Nodes)
            {
                if (node.Name.ToLower() == NodeName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        XmlNode MergeNodes(XmlNode A, XmlNode B, XmlDocument TargetDoc)
        {
            var merged = TargetDoc.CreateNode(A.NodeType, A.Name, A.NamespaceURI);

            foreach (XmlAttribute attrib in A.Attributes)
                merged.Attributes.Append(TargetDoc.CreateAttribute(attrib.Prefix, attrib.LocalName, attrib.NamespaceURI));

            var fromA = A.Attributes.Cast<XmlAttribute>();

            var fromB = B.Attributes.Cast<XmlAttribute>();

            var toAdd = fromB.Where(attr => fromA.Where(ata => ata.Name == attr.Name).Count() == 0);

            foreach (var attrib in toAdd)
                merged.Attributes.Append(TargetDoc.CreateAttribute(attrib.Prefix, attrib.LocalName, attrib.NamespaceURI));

            var childsFromA = A.ChildNodes.Cast<XmlNode>();
            var childsFromB = B.ChildNodes.Cast<XmlNode>();

            var uniquesFromA = childsFromA.Where(ch => childsFromB.Where(chb => chb.Name == ch.Name).Count() == 0);
            var uniquesFromB = childsFromB.Where(ch => childsFromA.Where(chb => chb.Name == ch.Name).Count() == 0);

            foreach (var unique in uniquesFromA)
                merged.AppendChild(DeepCloneToDoc(unique, TargetDoc));

            foreach (var unique in uniquesFromA)
                merged.AppendChild(DeepCloneToDoc(unique, TargetDoc));

            var Duplicates = from chA in childsFromA
                             from chB in childsFromB
                             where chA.Name == chB.Name
                             select new { A = chA, B = chB };

            foreach (var grp in Duplicates)
                merged.AppendChild(MergeNodes(grp.A, grp.B, TargetDoc));

            return merged;
        }

        XmlNode DeepCloneToDoc(XmlNode NodeToClone, XmlDocument TargetDoc)
        {

            var newNode = TargetDoc.CreateNode(NodeToClone.NodeType, NodeToClone.Name, NodeToClone.NamespaceURI);

            foreach (XmlAttribute attrib in NodeToClone.Attributes)
                newNode.Attributes.Append(TargetDoc.CreateAttribute(attrib.Prefix, attrib.LocalName, attrib.NamespaceURI));

            foreach (XmlNode child in NodeToClone.ChildNodes)
                newNode.AppendChild(DeepCloneToDoc(NodeToClone, TargetDoc));

            return newNode;

        }

        //xml helper end
    }
}
