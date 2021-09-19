﻿// Decompiled with JetBrains decompiler
// Type: Tools.ReStr
// Assembly: Tools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7664DE95-CB1F-45A9-9E49-805BE209CFAA
// Assembly location: F:\Developer\radegast\Radegast\assemblies\Tools.dll

using System.IO;

namespace Tools
{
  internal class ReStr : Regex
  {
    public string m_str = "";

    public ReStr()
    {
    }

    public ReStr(TokensGen tks, string str)
    {
        this.m_str = str;
        foreach (var c in str)
            tks.m_tokens.UsingChar(c);
    }

    public ReStr(TokensGen tks, char ch)
    {
      this.m_str = new string(ch, 1);
      tks.m_tokens.UsingChar(ch);
    }

    public override void Print(TextWriter s)
    {
      s.Write($"(\"{(object)this.m_str}\")");
    }

    public override int Match(string str, int pos, int max)
    {
      int length = this.m_str.Length;
      if (length > max || length > max - pos)
        return -1;
      for (int index = 0; index < length; ++index)
      {
        if ((int) str[index] != (int) this.m_str[index])
          return -1;
      }
      return length;
    }

    public override void Build(Nfa nfa)
    {
      int length = this.m_str.Length;
      NfaNode nfaNode = (NfaNode) nfa;
      for (int index = 0; index < length; ++index)
      {
        NfaNode next = new NfaNode(nfa.m_tks);
        nfaNode.AddArc(this.m_str[index], next);
        nfaNode = next;
      }
      nfaNode.AddEps(nfa.m_end);
    }
  }
}
