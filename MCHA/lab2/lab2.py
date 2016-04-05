#!/usr/bin/python2.7

import numpy


def simpleIterations(A, B, c, e):

    xk = numpy.zeros((len(A), 1))
    xk = c
    x = numpy.dot(B, xk) + c
    numberOfIterations = 0
    while abs(max((x - xk).flatten())) > e:
        xk = x
        x = numpy.dot(B, xk) + c
        numberOfIterations += 1

    print 'Simple iterations method. Result:'
    print x
    print 'Number of iterations {}'.format(numberOfIterations)


def zeidel(A, B, c, e):
    yk = numpy.zeros((len(A), 1))
    yk = c
    y = numpy.dot(B, yk) + c
    numberOfIterations = 1
    while abs(max((y - yk).flatten())) > e:
        yk = y
        for i in range(len(A)):
            y[i] = 0
            for j in range(i):
                y[i] = y[i] + B[i][j] * y[j]

            for j in range(i, len(A)):
                y[i] = y[i] + B[i][j] * yk[j]

            y[i] = y[i] + c[i]

        numberOfIterations += 1

    print 'Zeidel method. Result:'
    print y
    print 'Number of interations {}'.format(numberOfIterations)


def main():
    print("Start working")
    A = numpy.array([[24.41, 2.42, 3.85],
                     [2.31, 31.49, 1.52],
                     [3.49, 4.85, 28.92]],
                    "f")
    b = numpy.array([[30.24],
                     [40.95],
                     [42.81]],
                    "f")
    c = numpy.zeros((len(A), 1))
    B = numpy.zeros((len(A), len(A)))
    e = 0.00001

    for i in xrange(len(A)):
        c[i] = b[i] / A[i][i]
        for j in xrange(len(A)):
            B[i][j] = -A[i][j] / A[i][i]

        B[i][i] = 0

    print B
    print c

    maxx = [0 for i in range(len(A))]
    for i in range(len(A)):
        for j in range(len(A)):
            maxx[i] = maxx[i] + abs(B[i][j])

    print("Convergence test result:")
    print max(maxx)

    simpleIterations(A, B, c, e)
    zeidel(A, B, c, e)

if __name__ == "__main__":
        main()
