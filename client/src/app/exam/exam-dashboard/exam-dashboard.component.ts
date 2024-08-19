import { Component, OnInit } from '@angular/core';
import { MarkdownService } from 'ngx-markdown';



@Component({
  selector: 'app-exam-dashboard',
  templateUrl: './exam-dashboard.component.html',
  styleUrls: ['./exam-dashboard.component.scss']
})
export class ExamDashboardComponent   implements OnInit {

  constructor(private markdownService: MarkdownService) { }

  ngOnInit() {
    // outputs: <p>I am using <strong>markdown</strong>.</p>
    console.log(this.markdownService.parse('I am using __markdown__.'));
  }
  markdown = `

markdown
# Markdown Comprehensive Example

Markdown is a versatile and easy-to-use markup language for creating well-structured documents. This example demonstrates a wide range of Markdown features.

## 1. Headings and Emphasis

Markdown supports six levels of headings:

# H1
## H2
### H3
#### H4
##### H5
###### H6

You can also add emphasis:

- *Italic* (using )
- **Bold** (using )
- ***Bold and Italic*** (using  or )

## 2. Blockquotes

> Markdown allows you to create blockquotes to highlight important text.

## 3. Lists

### Unordered List

- Item 1
- Item 2
  - Subitem 2.1
  - Subitem 2.2
- Item 3

### Ordered List

1. First item
2. Second item
   1. Subitem 2.1
   2. Subitem 2.2
3. Third item

## 4. Code and Syntax Highlighting

Inline code: 

Code block with syntax highlighting:



## 5. Horizontal Rule

Create a horizontal rule with three or more hyphens:

---

## 6. Links and Images

### Links

- Inline link: [OpenAI](https://www.openai.com)
- Reference link: [OpenAI][1]

[1]: https://www.openai.com

### Images

![Markdown Logo](https://markdown-here.com/img/icon256.png)

## 7. Tables

| Syntax    | Description |
| --------- | ----------- |
| Header 1  | Content 1   |
| Header 2  | Content 2   |

## 8. Task Lists

- [x] Task 1
- [ ] Task 2
- [ ] Task 3

## 9. Strikethrough

~~This text is strikethrough.~~

## 10. Footnotes

Markdown supports footnotes[^1].

[^1]: This is a footnote.

## 11. Inline HTML

<strong>This is bold text using HTML</strong>

## 12. Escaping Characters

Use a backslash to escape characters: \*literal asterisks\*

## 13. Automatic URL Linking

Simply type a URL to create a link: https://www.openai.com

---

This document demonstrates a broad spectrum of Markdown's capabilities, showing how to format text effectively and elegantly.


This single document includes headings, emphasis, blockquotes, lists, code blocks, horizontal rules, links, images, tables, task lists, strikethrough, footnotes, inline HTML, character escaping, and automatic URL linking—all within one example.

  `;
}
