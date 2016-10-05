from methods import _vertStrips_
from colour import *

import PIL.Image as Image
import PIL.ImageDraw as ImageDraw
import datetime
import random
import math

size = (2560, 1600)


def colouredStrips():
    heightOld = 0
    height = random.randint(50, 200)

    # for saturation in range(0, 101, 10):
    #     for lightness in range(0, 101, 10):
    h, s, l = random_hsl()
    h=0
    img = Image.new("RGB", size, hsl(h, s, l))

    draw = ImageDraw.Draw(img)


    while (heightOld < size[1]):
        for p in _vertStrips_(0, heightOld, size[0], height, factorMin=30, factorMax=100):
            draw.polygon(p, modify_hsl(h, s, l, factor=15))

        heightOld = height
        height += random.randint(50, 200)
        h = random.randint(0, 360)

    # img.show
    filename = "s" + str(s) + "l" + str(l) + ".PNG"
    print filename
    img.save(filename, "PNG")



def __main__():
    for i in range(0, 40):
        print i
        colouredStrips()

__main__()
