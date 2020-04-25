from flask import Flask, json, request, jsonify
from sympy import *
from sympy.parsing.sympy_parser import parse_expr

import json
import jsonpickle
from json import JSONEncoder

class Equation:
    def __init__(self, leftSide, rightSide, equationLatex, solution, solutionLatex):
        self.LeftSide = leftSide
        self.RightSide = rightSide
        self.EquationLatex = equationLatex
        self.Solution = solution
        self.SolutionLatex = solutionLatex

def UseSympyToSolve(left, right):
    x = Symbol('x')
    y = Function('y')(x)
    dydx = y.diff(x)
    parsedLeft = parse_expr(left)
    parsedRight = parse_expr(right)
    print(parsedLeft)
    print(parsedRight)
    expr = Eq(parsedLeft, dydx)
    toLatex = latex(expr)
    sol = dsolve(expr, simplify=False)
    solToLatex = latex(sol)
    solu = str(sol)
    equation = Equation(left, right, toLatex, solu, solToLatex)
    return equation

def UseSympyToScramble(equation, maxVar):
    x = Symbol('x')
    y = Function('y')(x)
    dydx = y.diff(x)
    parsedEquation = parse_expr(equation)
    expr = Eq(parsedEquation)
    expr = expr * (x ** maxVar)
    return expr


api = Flask(__name__)


@api.route('/solve', methods=['POST'])
def GetSolved():
    data = request.get_json()
    left = data.get('LeftSide')
    print(left)
    right = data.get('RightSide')
    print(right)
    result = UseSympyToSolve(left, right)
    eqJSON = jsonpickle.encode(result, unpicklable=False)
    return eqJSON

@api.route('/scramble', methods=['POST'])
def GetScrambled():
    data = request.get_json()
    left = data.get('Expression')

    return jsonify(UseSympyToScramble(left))


if __name__ == '__main__':
    api.run()

