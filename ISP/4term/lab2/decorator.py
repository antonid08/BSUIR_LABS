def cached(func):
    cache = dict()

    def check(*args, **kwargs):
        if args in cache:
            print 'return cached result'
            return cache[args]

        result = func(*args, **kwargs)
        cache[args] = result
        return result
    return check


@cached
def foo(arg1, arg2):
    return arg1 + arg2


def main():
    print 'First call foo(1, 2):'
    print foo(1, 2)
    print 'First call foo(2, 3):'
    print foo(2, 3)

    print 'Second call foo(1, 2):'
    print foo(1, 2)
    print 'Second call foo(2, 3):'
    print foo(2, 3)

if __name__ == '__main__':
    main()
