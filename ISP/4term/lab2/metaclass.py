def parseAttrs(fileName):
    attrs = {}
    with open(fileName) as attrsFile:
        for line in attrsFile:
            lst = line.split('=', 1)
            attrs[lst[0]] = lst[1][:-1]
    return attrs


class MyMetaclass(type):
    def __new__(cls, name, bases, dct):
        dct.update(parseAttrs(cls.fileName))
        return super(MyMetaclass, cls).__new__(cls, name, bases, dct)


def MyMeta(filename):
    MyMetaclass.fileName = filename
    return MyMetaclass


class Boo(object):
    __metaclass__ = MyMeta('attrs')
    foo = 1


def main():
    print Boo.foo
    print Boo.fileAttribute

if __name__ == '__main__':
    main()
