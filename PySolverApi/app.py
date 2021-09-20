from flask import Flask, json, request, jsonify
from sympy import *
from sympy.parsing.sympy_parser import parse_expr
from sympy.abc import x,y

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

class Expression:
    def __init__(self, expression, maxVar):
        self.Expression = expression
        self.MaxVar = maxVar

class CheckExpr:
    def __init__(self, leftSide, rightSide, isUserSolutionTrue):
        self.LeftSide = leftSide
        self.RightSide = rightSide
        self.IsUserSolutionTrue = isUserSolutionTrue
    

def UseSympyToSolve(left, right):
    x = Symbol('x')
    y = Function('y')(x)
    dydx = y.diff(x)
    parsedLeft = parse_expr(left)
    parsedRight = parse_expr(right)
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
    exprStr = str(expr)
    expression = Expression(exprStr, maxVar)
    return expression

def UseSympyToCheck(leftSide, rightSide, isTrue):
    parsedLeft = parse_expr(leftSide)
    parsedRight = parse_expr(rightSide)
    isTrue = parsedLeft.equals(parsedRight)
    checkExpression = CheckExpr(leftSide, rightSide, isTrue)
    return checkExpression



api = Flask(__name__)


@api.route('/solve', methods=['POST'])
def GetSolved():
    print("solver api called")
    data = request.get_json()
    left = data.get('LeftSide')
    right = data.get('RightSide')
    result = UseSympyToSolve(left, right)
    eqJSON = jsonpickle.encode(result, unpicklable=False)
    print("equation solved")
    return eqJSON

@api.route('/scramble', methods=['POST'])
def GetScrambled():
    print("scramble api called")
    data = request.get_json()
    left = data.get('Expression')
    maxVar = data.get('MaxVar')
    result = UseSympyToScramble(left, maxVar)
    exJSON = jsonpickle.encode(result, unpicklable=False)
    return exJSON

@api.route('/check', methods=['POST'])
def GetChecked():
    print("check api called")
    data = request.get_json()
    left = data.get('UserSolution')
    right = data.get('RealSolution')
    istrue = data.get('isUserSolutionTrue')
    result = UseSympyToCheck(left, right, istrue)
    exJSON = jsonpickle.encode(result, unpicklable=False)
    return exJSON


if __name__ == '__main__':
    api.run(port=5050)

