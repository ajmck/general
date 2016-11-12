import random
import math

""" Different methods for generating random imagery """

def gen_vertStrips(size):
    return _vertStrips_(0, 0, size[0], size[1])


def _vertStrips_(minX, minY, maxX, maxY, factorMin=45, factorMax=45):
    points = []
    factor = random.randint(factorMin, factorMax)
    p1 = (minX, minY)
    p2 = (minX, maxY)
    p3 = (factor, minY)

    while p1[0] < maxX + factor:
        points.append((p1, p2, p3))
        p1 = p2
        p2 = p3
        if factorMin != factorMax:
            p3 = (p1[0] + random.randint(factorMin, factorMax), p1[1])
        else:
            p3 = (p1[0] + factor, p1[1])

    return points


def gen_squareangles(size):
    return __squareangles__(0, 0, size[0], size[1])


def __squareangles__(minX, minY, maxX, maxY):
    points = []
    factor = 80
    height = minY + factor

    p1 = (minX, minY)
    p2 = (minX, height)
    p3 = (minX + factor, minY)


    while height < maxY + factor:
        while p1[0] < maxX + factor:
            points.append((p1, p2, p3))
            p1 = p2
            p2 = p3
            p3 = (p1[0] + factor, p1[1])
            # print p1, p2, p3
        # print "New Row"
        p1 = (minX, height)
        p2 = (minX, height + factor)
        p3 = (minX + factor, height)

        height += factor

    return points


def gen_centred(size):
    return __centred__(0, 0, size[0], size[1])


def __centred__(minX, minY, maxX, maxY):
    points = []

    centroid = (random.randint(minX, maxX), random.randint(minY, maxY))
    horz = minX
    vert = minY

    N, E, S, W = False, False, False, False
    p1 = (minX, minY)

    while not (N and E and S and W):
        # # old format before refactoring (pseudocode)
        # # essentially, go around the edge of the picture
        # # when hitting an edge, do the next side

        # if not (direction)
        #     N and S change horizontal
        #     E and W change vertical
        #
        #     S and W change by a negative value
        #     if (horz or vert) > respective size
        #         horz or vert = edge (0 or respective size)

        if not N:
            horz += random.randint(20, 200)

            if horz > maxX:
                horz = maxX
                N = True
                # north done

        elif not E:
            vert += random.randint(20, 200)

            if vert > maxY:
                vert = maxY
                E = True
                # east done
        elif not S:
            horz -= random.randint(20, 200)

            if horz < minX:
                horz = minX
                S = True
                # south done
        elif not W:
            vert -= random.randint(20, 200)

            if vert < minY:
                vert = minY
                W = True

        p2 = (horz, vert)
        points.append((p1, p2, centroid))
        p1 = p2

    return points


def gen_1(size):
    return __g1__(0, 0, size[0], size[1])


def __g1__(minX, minY, maxX, maxY):
    points = []
    r = random.randint(3, 100)
    p1 = (minX, minY)
    p2 = (minX, r)
    p3 = (r, minY)

    height = r

    while height < maxY:
        while p1[0] < maxX:
            points.append((p1, p2, p3))
            p1 = p2
            p2 = p3
            p3 = (p1[0] + random.randint(3, 100), p1[1])
            # print p1, p2, p3
        # print "New Row"
        p1 = (minX, height)
        p2 = (minX, height + random.randint(3, 100))
        p3 = (45, height)

        height += random.randint(3, 100)

    return points



def gen_parallelogram(size, slant=30):
    return __parallelogram__(0, 0, size[0], size[1], factor=slant)


def __parallelogram__(minX, minY, maxX, maxY, factor=30):
    points = []
    currX = minX
    currY = minY
    
    while currY < maxY + factor:
        while currX < maxX + factor:
            p1 = (currX, currY)
            p2 = (currX - factor, currY + factor)
            p3 = (currX, currY + factor)
            p4 = (currX + factor, currY)
            points.append((p1, p2, p3, p4))
            
            currX += factor
        currX = 0
        currY += factor
    
    return points


def gen_tetris(size, factor=30):
    return __tetris__(0, 0, size[0], size[1], factor)

def __tetris__(minX, minY, maxX, maxY, factor=30):
    points = []
    currX = minX
    currY = minY
    
    while currY < (maxY + factor):
        while currX < (maxX + factor):
            p1 = (currX, currY)
            p2 = (currX, currY + factor)
            p3 = (currX - factor, currY + factor)
            p4 = (currX - factor, currY + 2*factor)
            p5 = (currX + factor, currY + 2*factor)
            p6 = (currX + factor, currY + factor)
            p7 = (currX + factor + factor, currY + factor)
            p8 = (currX + factor + factor, currY)
            points.append((p1, p2, p3, p4, p5, p6, p7, p8)) 
            currX += 2 * factor
        currX = 0
        currY += 2 * factor
    return points


def gen_centredSquare(size):
    return __centredSquare__(0, 0, size[0], size[1])


def __centredSquare__(minX, minY, maxX, maxY, minFactor=30, maxFactor=300):
    centroid = (random.randint(minX, maxX), random.randint(minY, maxY))
    length = (maxX - minX) + (maxY - minY)
    points = []

    pN = (centroid[0], centroid[1] + length)
    pE = (centroid[0] + length, centroid[1])
    pS = (centroid[0], centroid[1] - length)
    pW = (centroid[0] - length, centroid[1])
    points.append((pN, pE, pS, pW))

    while (length > 0):
        factor = random.randint(minFactor, maxFactor)
        pN = (pN[0], pN[1] - factor)
        pS = (pS[0], pS[1] + factor)
        pE = (pE[0] - factor, pE[1])
        pW = (pW[0] + factor, pW[1])
        points.append((pN, pE, pS, pW))
        length -= factor
    
    return points
