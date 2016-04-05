import sympy as sp


def solve_gaus(A, b):
    c = A.cols
    for k in range(A.rows - 1):
        for i in range(1 + k, A.rows):
            temp = A[c * i + k]
            b[i] = b[i] - temp / A[k, k] * b[k]
            for j in range(k, A.cols):
                A[i, j] -= temp / A[k, k] * A[k, j]

    x = [sp.Symbol("x" + str(i)) for i in range(1, c + 1)]

    for i in range(A.rank() - 1, -1, -1):
        x[i] = b[i]
        for j in range(A.cols - 1, i, -1):
            x[i] -= A[i, j] * x[j]

        x[i] /= A[i, i]

    return x


def not_null(A):
    det = A.det()
    return sp.solve(det, sp.Symbol('a'))


def main():
    a = sp.Matrix((
        [2, -1, -2],
        [0, 2, 1],
        [-2, -2, 2]
    ))
    b = sp.Matrix(([-1], [3], [-2]))
    res = solve_gaus(a, b)
    sp.pprint(res)

    a2 = sp.Matrix((
        [2, 1, -3],
        [2, 3, 1],
        [3, 2, 1]
    ))
    b2 = sp.Matrix(([7], [1], [6]))
    res2 = solve_gaus(a2, b2)
    sp.pprint(res2)

    a3 = sp.Matrix((
        [2, -2, 5, 1],
        [1, -2, 3, 0],
        [3, -4, 8, 1]
    ))
    b3 = sp.Matrix(([11], [4], [15]))

    res3 = solve_gaus(a3, b3)
    sp.pprint(res3)

    a4 = sp.Matrix((
        [1, -2],
        [2, -4]
    ))
    b4 = sp.Matrix(([0], [0]))
    res4 = solve_gaus(a4, b4)
    sp.pprint(res4)

    a = sp.Symbol('a')
    a5 = sp.Matrix((
        [5, -3, 4],
        [a, 2, -1],
        [8, -1, a]
    ))

    print not_null(a5)


if __name__ == '__main__':
    main()
