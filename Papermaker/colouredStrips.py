import methods
from colour import *

import PIL.Image as Image
import PIL.ImageDraw as ImageDraw
import datetime
import random
import math

size = (720, 1280)


def colouredStrips():
    heightOld = 0
    intervals = range(80, 321, 80)
    height = random.choice(intervals)
    factor = random.randint(10, 30)

    # for saturation in range(0, 101, 10):
    #     for lightness in range(0, 101, 10):
    h, s, l = random_hsl()
    h=random.randint(0, 360)
    img = Image.new("RGB", size, hsl(h, s, l))

    draw = ImageDraw.Draw(img)


    while (heightOld < size[1]):
        for p in methods._vertStrips_(0, heightOld, size[0], height):
            draw.polygon(p, modify_hsl(h, s, l, factor=factor))

        factor = random.randint(10, 30)
        heightOld = height
        height += random.choice(intervals)

        s = random.randint(20, 100)
        l = random.randint(0, 100)


    # img.show
    #filename = "s" + str(s) + "l" + str(l) + ".PNG"
    filename = "h" + str(h) + ".png"
    print filename
    img.save(filename, "PNG")



def __main__():
    for i in range(0, 40):
        print i
        colouredStrips()

__main__()
