// (Partial file, for demonstration purposes)
//
//  Reader.h
//  Copyright © 2018 BNotro Software Development. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "ReaderTable.h"

NS_ASSUME_NONNULL_BEGIN

@interface Reader : NSObject
+ (NSMutableArray *) loadFromTxtResource: (NSString *) filename;
+ (NSMutableArray *) loadFromUrl: (NSURL *) menuUrl;
+ (NSMutableArray *) loadManifest:( NSString *) content;
@end

NS_ASSUME_NONNULL_END
