"use client";

import Editor from "@monaco-editor/react";

const defaultCode = `public class Solution
{
    public int Add(int a, int b)
    {
        // Write your code here
    }
}`;

export default function CodeEditor() {
  return (
    <div className="rounded-lg overflow-hidden border border-gray-700">
      <Editor
        height="400px"
        defaultLanguage="csharp"
        defaultValue={defaultCode}
        theme="vs-dark"
        options={{
          minimap: { enabled: false },
          fontSize: 14,
          lineNumbers: "on",
          scrollBeyondLastLine: false,
          automaticLayout: true,
        }}
      />
    </div>
  );
}
