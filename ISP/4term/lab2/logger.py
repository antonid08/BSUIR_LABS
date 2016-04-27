class Logger(object):
    def __init__(self):
        self.__logs__ = []

    def __getattribute__(self, attr):
        if hasattr(object.__getattribute__(self, attr), '__call__'):
            def log(*args, **kwargs):
                result = object.__getattribute__(self, attr)(*args, **kwargs)
                self.__logs__.append((attr, args, kwargs, result))
                return result
            return log
        else:
            return object.__getattribute__(self, attr)

    def __str__(self):
        return '\n'.join(['Called: {0}. Arguments: {1} {2}. Result: {3}\n'.
                          format(name, args, kwargs, result) for
                          name, args, kwargs, result in self.__logs__])


class Boo(Logger):
    k = 1

    def foo(self, arg1):
        return arg1 * 2


def main():
    f = Boo()
    f.k = 0
    print f.foo(2)
    print f.foo(3)

    print str(f)

if __name__ == '__main__':
    main()
