//
//  main.m
//
//  Copyright © 2018 BNotro Software Development Pty Ltd. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Reader.h"

int main(int argc, const char * argv[]) {
	@autoreleasepool {
		
		// make sure data.txt is included in project: Build Phases > Copy Files, (Resources), +add, uncheck 'copy only when installing'.
		NSMutableArray * tables = [Reader loadFromTxtResource:@"data.txt"];
		
		for (ReaderTable * table in tables)
		{
			for (ReaderRow * row in table.rows)
			for (NSInteger i = 0, u = table.header.count-1; i <= u; i++)
			{
				NSObject * data = row.values[table.header[i]];
				NSLog(@"%@: %@", table.header[i],data);
			}
		}

		ReaderTable * atable = tables[0];
		ReaderRow * arow= atable.rows[1];
		NSString * value= arow.values[@"Type"];
		NSLog(@"Hello, %@!", value);
	}
	return 0;
}
