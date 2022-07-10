# Manifest
A simple, compact way to represent data tables in a format friendly to both humans and machines, suitable for both file storage and streaming. 

<h1><strong>Table Data Notation</strong></h1>

<p>(BNotro Software Development.)<br />
Format revision:&nbsp;3.3</p>

<p>License: Creative Commons Attribution -or- MIT License​​​​​​</p>

<p><strong>SUMMARY</strong></p>

<p>A fast, compact data format alternative to JSON and XML, suited&nbsp;to simple scenarios.</p>
<p>It is intended to be generally compatible with <a href="https://www.markdownguide.org/extended-syntax/">Markdown</a> tables so long as the column headings are enclosed in square brackets [<em>thus</em>].<sup>3.3</sup></p>

<table border="0" cellpadding="0" cellspacing="0" style="width:433px; margin-left:40px;" width="435">
	<tbody>
		<tr height="21">
			<th height="21" style="height: 21px; text-align: center;">[Type]</th>
			<th style="text-align: center;">[Weight]</th>
			<th style="text-align: center;">[Cost]</th>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Apple</td>
			<td style="text-align: center;">0.1</td>
			<td style="text-align: center;">$0.10</td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Pear</td>
			<td style="text-align: center; color: goldenrod;">..</td>
			<td style="text-align: center; color: goldenrod;">..</td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Banana</td>
			<td style="text-align: center;">0.2</td>
			<td style="text-align: center;">$0.30</td>
		</tr>
	</tbody>
</table>

<p><br />
<b>AUTHOR</b></p>

<p>Andrew Kingdom,&nbsp;BNotro Software Development &copy;&nbsp;2017-2022</p>

<p></p>

<p><strong>SOURCE CODE</strong></p>

<ul>
	<li><a href="https://github.com/akingdom/Manifest/tree/main/Objective-C%20Example">Objective-C</a></li>
	<li><a href="https://github.com/akingdom/Manifest/tree/main/C%23%20Example">C#</a></li>
</ul>

<p></p>

<p><strong>INTRODUCTION</strong></p>

<p>This document presents a simple, compact way to represent&nbsp;data tables in a format friendly to both humans and machines,&nbsp;suitable for both file storage and streaming.</p>

<p>The format is influenced by&nbsp;many existing formats that were reviewed in the process of defining it.&nbsp;</p>

<p>The format has been used successfully&nbsp;in use in a number of&nbsp;real-world&nbsp;projects.</p>

<p>The aims are&nbsp;for compactness, robustness, readability, editability and simplicity of programming needed to parse it.</p>

<p>For simple data requirements this format is well suited as an alternative to the XML, JSON, tab delimited&nbsp;or comma delimited&nbsp;standards.</p>

<p>The format can also be used for marking or embedding data in text documents as a basic&nbsp;alternative to HTML or Markdown.</p>

<p></p>

<p><strong>REQUIREMENTS and RECOMMENDATIONS</strong></p>

<ul>
	<li>Requirement: Human readability, machine readability and not getting destroyed (too much) by Excel and text editors (automatic tab-to-space conversion is not friendly to this).</li>
	<li>Requirement: All header rows must start with a left-square-bracket</li>
	<li>Requirement: All header keys are enclosed within square brackets [thus].</li>
	<li>Requirement: A header row only contains headers</li>
	<li>Requirement: Multiple headers within a row are separated by a delimiter character (e.g. tab 0x9 or vertical-bar 0x7c)</li>
	<li>Requirement: A subsequent header row marks the start of a new table</li>
	<li>Requirement: Value rows/columns follow a header row</li>
	<li>Requirement: Values may be omitted (and are filled in during processing as empty strings) - that is, data rows may be ragged.</li>
	<li>Requirement: Excess values are ignored and not read (value columns with no corresponding header)</li>
	<li>Requirement: Any headers from a previous table are ignored.</li>
	<li>Requirement: Duplicate headers within a table are ignore (the whole column)</li>
	<li>Requirement: The delimiter between the first two header cells of a table is assumed to be that used for the entire rest of the table. This is a simple way to work with various delimiters.</li>
	<li>Recommendation: A cell value equal to two period marks only (&nbsp;<span style="color: goldenrod;">..</span> 0x2e2e) is a ditto mark that copies the value of previous value (above) in that column. The ditto mark in a first row is parsed as an empty string. This is fast to type and is not destroyed by Excel, etc.</li>
	<li>Recommendation: Initial rows prior to any header row are to be parsed as a table with an empty string as the implied header key.</li>
	<li>Recommendation: Header names are only alphanumeric or spaces.</li>
	<li>Recommendation: There are no reserved header key names -- this is up to the receiver of the assembled/parsed data to build on top of this standard (and separate from it)</li>
	<li>Recommendation: Any relationships between multiple tables in a file is completely arbitrary and up to the receiver of the assembled/parsed data.</li>
	<li>Recommendation: When Table Data Notation is used as an extension to the Markdown standard, it should ideally replace all other table format extensions -or- also recognise and process the most common Markdown table format extension.<sup>3.3</sup></li>
</ul>

<p></p>

<p><strong>KNOWN WEAKNESSES</strong></p>

<p>Currently there is no standard way to include delimiters within values (no delimiter escape mechanism)</p>

<p>This could be done grep (backslash&hellip;) and/or possibly html (ampersand character-name semicolon), but has not been a requirement yet. Suggestions welcome.</p>

<p>Deliberately there is no structural nesting, as per HTML: <code>&lt;i&gt;&lt;b&gt;value&lt;/b&gt;&lt;/i&gt;</code></p>

<p>Deliberately there is no equivalent to HTML attributes thusly: <code>&lt;a href=url&gt;</code></p>

<p></p>

<p></p>

<table border="0" cellpadding="0" cellspacing="0" style="width: 433px;" width="435">
	<colgroup>
		<col span="5" />
	</colgroup>
	<tbody>
		<tr>
			<td height="21" style="height: 21px; text-align: center;">[<strong>EXAMPLES</strong>]</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr>
			<td colspan="5" height="21" style="height: 21px;"></td>
		</tr>
		<tr>
			<td colspan="5" height="21" style="height: 21px;">Here are a few examples to demonstrate the format.</td>
		</tr>
		<tr>
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr>
			<td height="21" style="height: 21px; text-align: center;"><span style="font-family:courier new,courier,monospace;">[Creature]</span></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Cat</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Dog</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Parrot</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Fish</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr>
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"><span style="font-family:courier new,courier,monospace;">[Type]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[Weight]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[Cost]</span></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Apple</td>
			<td style="text-align: center;">0.1</td>
			<td style="text-align: center;">$0.10</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Banana</td>
			<td style="text-align: center;">0.2</td>
			<td style="text-align: center;">$0.30</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Pear</td>
			<td style="text-align: center;">0.15</td>
			<td style="text-align: center;">$0.15</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"><span style="font-family:courier new,courier,monospace;">[Person]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[Score]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[Note]</span></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Bob</td>
			<td style="text-align: center;">10</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Bob</td>
			<td style="text-align: center;">40</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Alice</td>
			<td style="text-align: center;">90</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Bob</td>
			<td style="text-align: center;">..</td>
			<td style="text-align: center;">90 score</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Mary</td>
			<td style="text-align: center;">40</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Alice</td>
			<td style="text-align: center;">83</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr>
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"><span style="font-family:courier new,courier,monospace;">[X]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[Y]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[Z]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[A]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[Note]</span></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;">No result</td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">4</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;">2</td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;">2</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;">4</td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;">No result</td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">0</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">1</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;">2</td>
			<td style="text-align: center;">2</td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;">3</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;">4</td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"><span style="font-family:courier new,courier,monospace;">[Id]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[space]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[m2]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[Note]</span></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">1</td>
			<td style="text-align: center;">Cattleyard</td>
			<td style="text-align: center;">50</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">2</td>
			<td style="text-align: center;">..</td>
			<td style="text-align: center;">100</td>
			<td colspan="2">Cattleyard space too</td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">3</td>
			<td style="text-align: center;">..</td>
			<td style="text-align: center;">120</td>
			<td colspan="2">ditto</td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">4</td>
			<td style="text-align: center;">..</td>
			<td style="text-align: center;">70</td>
			<td colspan="2">ditto</td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">5</td>
			<td style="text-align: center;">Office</td>
			<td style="text-align: center;">16</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">6</td>
			<td style="text-align: center;">..</td>
			<td style="text-align: center;">12</td>
			<td colspan="2">Office space too</td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">7</td>
			<td style="text-align: center;">..</td>
			<td style="text-align: center;">10</td>
			<td colspan="2">ditto</td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr>
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"><span style="font-family:courier new,courier,monospace;">[Table]</span></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Meat</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"><span style="font-family:courier new,courier,monospace;">[Name]</span></td>
			<td style="text-align: center;"><span style="font-family:courier new,courier,monospace;">[Cost]</span></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Beef</td>
			<td style="text-align: center;">$3.50</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Ham</td>
			<td style="text-align: center;">$2.00</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;">Turkey</td>
			<td style="text-align: center;">$2.50</td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
		<tr height="21">
			<td height="21" style="height: 21px; text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
			<td style="text-align: center;"></td>
		</tr>
	</tbody>
</table>

<p></p>

<p></p>

<p></p>


---
