﻿/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. All rights reserved.
 *
 * This software is subject to the Microsoft Public License (Ms-PL). 
 * A copy of the license can be found in the license.htm file included 
 * in this distribution.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * ***************************************************************************/

using System.Web.Razor;

namespace System.Web.Mvc.Razor
{
	using System.Diagnostics;
	using System.Web.Razor.Generator;
	using System.Web.Razor.Parser;

	public class MvcWebPageRazorHost : RazorEngineHost
	{
		protected MvcWebPageRazorHost()
		{
			Init();
		}

		public MvcWebPageRazorHost(RazorCodeLanguage codeLanguage) 
			: base(codeLanguage)
		{
			Init();			
		}

		public MvcWebPageRazorHost(RazorCodeLanguage codeLanguage, Func<MarkupParser> markupParserFactory) 
			: base(codeLanguage, markupParserFactory)
		{
			Init();			
		}

		private void Init()
		{
			GetRidOfNamespace("System.Web.WebPages.Html");
		}

		public override RazorCodeGenerator DecorateCodeGenerator(RazorCodeGenerator incomingCodeGenerator)
		{
			if (incomingCodeGenerator is RazorEngine.Compilation.CSharp.CSharpRazorCodeGenerator)
			{
				return new RazorEngine.Compilation.CSharp.CSharpRazorCodeGenerator(incomingCodeGenerator.ClassName,
					incomingCodeGenerator.RootNamespaceName,
					incomingCodeGenerator.SourceFileName,
					incomingCodeGenerator.Host);
			}
			return base.DecorateCodeGenerator(incomingCodeGenerator);
		}
		 
		public override ParserBase DecorateCodeParser(ParserBase incomingCodeParser)
		{
			if (incomingCodeParser is CSharpCodeParser)
			{
				return new MvcCSharpRazorCodeParser();
			}
			else
			{
				return base.DecorateCodeParser(incomingCodeParser);
			}
		}

		private void GetRidOfNamespace(string ns)
		{
			Debug.Assert(NamespaceImports.Contains(ns), ns + " is not a default namespace anymore");
			if (NamespaceImports.Contains(ns))
			{
				NamespaceImports.Remove(ns);
			}
		}
	}

}
