The SVG assets that have been included in this commit (the cta_primary etc.) have been modified from the originals.
It was found that the height/width attributes needed removing on the svg in order for the svg to stretch within its container. 
Also the aspect ratio attribute had to be set to none.

Use background-size: 100% 100%; in conjunction with the above mentioned attributes so that the svg's scale properly

From StackOverflow:

Also since you are using preserveAspectRatio="none" the stroke may get irregular since it may be stretched differently on x and on y. 
The solution to this problem is using vector-effect='non-scaling-stroke'