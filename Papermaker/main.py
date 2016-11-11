import PIL.Image as Image
import PIL.ImageDraw as ImageDraw
import datetime
import random
import math

# Custom methods
from methods import * 
from colour import *

# filename = random.randint(10000000, 99999999)
size = (2560, 1600)  # width, height



def create(points, color=None):
    # Set base colour
    if color is not None:
        h, s, l = color
    else:
        h, s, l = random_hsl()
    
    # Create an initial, empty PIL image
    img = Image.new("RGB", size, hsl(h, s, l))
    draw = ImageDraw.Draw(img)
    
    # Draw each polygon
    for p in points:
        draw.polygon(p, modify_hsl(h, s, l, factor=15))
    
    # Save (or display)
    # img.show
    img.save(str(datetime.datetime.now()) + ".PNG", "PNG")



def __main__():
    for i in range(10):
        create(gen_3(size), random_hsl() )
        print (i)


__main__()
