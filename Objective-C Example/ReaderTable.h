//
//  ReaderTable.h
//
//  Copyright © 2018 BNotro Software Development Pty Ltd. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "ReaderRow.h"

NS_ASSUME_NONNULL_BEGIN

@interface ReaderTable : NSObject

@property (strong, nonatomic) NSMutableArray * header;  // of NSString
@property (strong, nonatomic) NSMutableArray * rows;  // of ReaderRow

@end

NS_ASSUME_NONNULL_END

/*
ReaderTable.m is an empty class...
#import "ReaderTable.h"
@implementation ReaderTable
@end
*/
