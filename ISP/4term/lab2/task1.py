import tempfile as tf


def sortBuffer(buff):
    tempfile = tf.TemporaryFile()
    sortedList = sorted(buff.split('\n'))
    tempfile.write('\n'.join(sortedList))
    return tempfile


def sort(fileName, bufferSize):
    with open(fileName, 'r') as unsortedFile:
        bufferSize -= bufferSize % len(unsortedFile.readline())
        unsortedFile.seek(0)

        tempFiles = []

        while True:
            buff = unsortedFile.read(bufferSize)
            if not buff:
                break
            tempFiles.append(sortBuffer(buff))

        print len(tempFiles)
        tempFiles[0].seek(0)
        print tempFiles[0].read()


def main():
    unsortedFileName = 'file'
    bufferSize = 500
    sort(unsortedFileName, bufferSize)


if __name__ == '__main__':
    main()
