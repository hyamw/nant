// NAnt - A .NET build tool
// Copyright (C) 2002 Scott Hernandez (ScottHernandez@hotmail.com)
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

// Scott Hernandez (ScottHernandez@hotmail.com)

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

using NUnit.Framework;
using SourceForge.NAnt.Tasks;
using SourceForge.NAnt.Attributes;

namespace SourceForge.NAnt.Tests {

    public class LoopTest : BuildTestBase {
        public LoopTest(String name) : base(name) {
        }

        public void Test_Loop_String_Default_Delim() {
            string _xml = @"
                    <project>
                        <foreach item='String' in='1,2,3,4' delim=',' property='count'>
                            <echo message='Count:${count}'/>
                        </foreach>
                    </project>";
            string result = RunBuild(_xml);
            //Log.WriteLine(result);
            Assert(result.IndexOf("Count:1") != -1);
            Assert(result.IndexOf("Count:2") != -1);
            Assert(result.IndexOf("Count:3") != -1);
            Assert(result.IndexOf("Count:4") != -1);
        }
        
        public void Test_Loop_Files() {
            string _xml = @"
                    <project>
                        <foreach item='File' in='${nant.project.basedir}' property='file'>
                            <echo message='File:${file}'/>
                        </foreach>
                    </project>";
            string result = RunBuild(_xml);
            //Log.WriteLine(result);
            Assert(result.IndexOf("test.build") != -1);
        }

        public void Test_Loop_Folders() {
            string _xml = @"
                    <project>
                        <mkdir dir='${nant.project.basedir}/foo'/>
                        <mkdir dir='${nant.project.basedir}/bar'/>
                        <foreach item='Folder' in='${nant.project.basedir}' property='folder'>
                            <echo message='Folder:${folder}'/>
                        </foreach>
                    </project>";
            string result = RunBuild(_xml);
            //Log.WriteLine(result);
            Assert(result.IndexOf("foo") != -1);
            Assert(result.IndexOf("bar") != -1);
        }

        public void Test_Loop_Lines() {
            string _xml = @"
                    <project>
                    <!-- Hello from inside -->
                        <foreach item='Line' in='${nant.project.basedir}/test.build' property='line'>
                            <echo message='Line:${line}'/>
                        </foreach>
                    </project>";
            string result = RunBuild(_xml);
            //Log.WriteLine(result);
            Assert(result.IndexOf("Hello") != -1);
        }
    }
}