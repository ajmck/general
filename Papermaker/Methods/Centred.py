import random


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
