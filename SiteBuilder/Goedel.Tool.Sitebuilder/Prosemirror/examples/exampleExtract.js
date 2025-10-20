

import { EditorState } from "prosemirror-state";
import { DOMParser } from "prosemirror-model";

// Assuming `editorView` is your ProseMirror editor instance
function postEditorContent(editorView) {
  // Extract the editor's content as JSON
  const contentJSON = editorView.state.doc.toJSON();

  // Send the JSON content to an HTTP endpoint
  fetch("https://your-api-endpoint.com/save", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ content: contentJSON }),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to post content");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Content successfully posted:", data);
    })
    .catch((error) => {
      console.error("Error posting content:", error);
    });
}
