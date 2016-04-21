import tempfile as tf


def sortBuffer(buff):
    tempfile = tf.TemporaryFile()
    sortedList = sorted(buff.split('\n'))
    tempfile.write('\n'.join(sortedList))
    return tempfile


def getSortedTempfiles(unsortedFile, bufferSize):
    tempFiles = []

    while True:
        buff = unsortedFile.read(bufferSize)
        if not buff:
            break
        tempFiles.append(sortBuffer(buff))

    return tempFiles


def merge(tempFiles):
    def merge(leftFile, rigthFile):
        result = tf.TemporaryFile();



    for index in [x for x in xrange(0, len(tempfiles) - 1 , 2)]:
        merge(tempFiles[index], tempFiles[index + 1]

def sort(fileName, bufferSize):
    with open(fileName, 'r') as unsortedFile:
        bufferSize -= bufferSize % len(unsortedFile.readline())
        unsortedFile.seek(0)

        tempFiles = getSortedTempfiles(unsortedFile, bufferSize)

    with open(resultFileName, 'w') as resultFile:
        resultFile.write(merge(tempfiles))



def main():
    unsortedFileName = 'file'
    bufferSize = 500
    sort(unsortedFileName, bufferSize)


if __name__ == '__main__':
    main()
