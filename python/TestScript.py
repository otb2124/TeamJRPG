"""func"""
def test() -> None:
    pass

def info(x: int) -> None:
    print(x, end='!')

def summa(x: int, y: int) -> int:
    res = x + y
    info(res)
    return res

res1 = summa(x=10, y=9.2)
print(res1)

"""aaaa"""
def min_func(args: list) -> float:
    min_value = args[0]
    for i in args:
        if i < min_value:
            min_value = i
        else:
            pass
        print(min_value)
    return min_value

elms = [10, 7, 8, 7.3, 3.3, 3.2, 1.2, -1.2, -8.9]
min_func(elms)

func = lambda x, y: x + y
print(func(3, 7))
