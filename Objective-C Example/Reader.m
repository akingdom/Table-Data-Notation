// (Partial file, for example purposes)
//
//  Reader.m
//  Copyright © 2018 BNotro Software Development. All rights reserved.
//


#import "Reader.h"

@implementation Reader

+ (NSMutableArray *) loadFromTxtResource: (NSString *) filename {
	NSString * ext = [filename pathExtension];
	NSString * name = [[filename lastPathComponent] substringToIndex:(filename.length - ext.length -1)];
	NSURL * menuURL = [[NSBundle mainBundle]URLForResource:name withExtension:ext];
	return [self loadFromUrl:menuURL];
}

+ (NSMutableArray *) loadFromUrl: (NSURL *) menuUrl {
	
	NSString * content = [[NSString alloc]initWithContentsOfURL:menuUrl
													   encoding:NSUTF8StringEncoding
														  error:nil];
	return [self loadManifest:content];
}

+ (NSMutableArray *) loadManifest:( NSString *) content
{
	NSMutableArray * tables = [NSMutableArray new];  // of ReaderTable
	
	ReaderTable * currentTable = nil;
	
	
	// Table entry specific properties...
	NSMutableDictionary * data;
	NSInteger subRowIndex;
	NSInteger tableIndex = -1;

	NSArray *guideLines = [content componentsSeparatedByString:@"\n"];
	
	NSString *dittomark = @"..";

	NSString *delimiter = @"";
	data= [[NSMutableDictionary alloc]init];
	NSMutableArray *headers = [[NSMutableArray alloc] initWithObjects:@"", nil];
	NSInteger headerCount = 1;
	subRowIndex = 0;
	
	for (NSInteger row= 0, u= [guideLines count]-1; row<=u; row++) {
		NSString * guideLine = [guideLines objectAtIndex:row];
		NSLog(@"LINE: '%@'",guideLine);
		
		bool isHeaderRow = ([guideLine hasPrefix: @"["]);
		
		// grab the delimiter
		if (isHeaderRow)
		{
			NSRange headerEnd = [guideLine rangeOfString:@"]"];
			NSRange seekNext = NSMakeRange(headerEnd.location + headerEnd.length, [guideLine length] - headerEnd.location - headerEnd.length);
			NSRange headerNext = [guideLine rangeOfString:@"[" options:NSLiteralSearch range:seekNext];
			if(headerNext.length == 0)
			{
				delimiter = @"|";
			}
			else
			{
				NSRange delimiterRange = NSMakeRange(headerEnd.location + headerEnd.length, headerNext.location - headerEnd.location - headerEnd.length);
				delimiter = [guideLine substringWithRange:delimiterRange];
			}
		}
		
		NSArray *cells= [guideLine componentsSeparatedByString:delimiter];
		NSInteger cellCount = [cells count];
		
		if(isHeaderRow)
		{
			tableIndex++;
			
			// save prior table
			
			currentTable = [ReaderTable new];
			
			headerCount= cellCount;  // track header count for data (non-header) rows
			headers= [[NSMutableArray alloc] initWithCapacity:cellCount];
			currentTable.header = headers;
			currentTable.rows = [NSMutableArray new];
			[tables addObject:currentTable];
			
			// copy cell values for this row as new headers
			for(NSInteger j = 0, v = cellCount-1; j<=v; j++)
			{
				NSString * headerInside= [[cells[j] stringByReplacingOccurrencesOfString:@"[" withString:@""] stringByReplacingOccurrencesOfString:@"]" withString:@""];  // strip the [] square brackets from the header
				headers[j]= headerInside;  // keep for use in data (non-header) rows
			}
			
			subRowIndex= 0;
		}
		else  // isDataRow assumed (!isHeaderRow)
		{
			// Add cell values to data
			// Excess values are ignored.
			ReaderRow * currentRow = [ReaderRow new];
			currentRow.values = [NSMutableDictionary new];
			[currentTable.rows addObject:currentRow];
			for (NSInteger col = 0,v=headerCount-1,w=cellCount-1; col<=v && col<=w; col++) {
				NSString *key = headers[col];
				NSString *value = cells[col];
				if(dittomark == value)
				{
					if (subRowIndex <= 2)
						[currentRow.values setObject:@"" forKey:key];
					else
					{
						ReaderRow * previousRow = [currentTable.rows lastObject];  // (last record -- this record hasn't been added yet)
						value = previousRow.values[key];
						[currentRow.values setObject:value forKey:key];  // repeat previous value
					}
				}
				else
				{
					[currentRow.values setObject:value forKey:key];
				}
			}
			// if row has fewer cells than data cells, fill in the missing data cells with blanks
			for (NSInteger col = cellCount,v=headerCount-1; col<=v; col++) {
				[currentRow.values setObject:@"" forKey:headers[col]];
			}
			
			subRowIndex++;
		}
	}
	
	// hand over entries
	return tables;
}


@end
