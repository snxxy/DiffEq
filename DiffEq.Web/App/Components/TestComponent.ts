import axios from "axios"
import MathJax from "MathJax"
import { computed, onUnmounted, ref, watch } from "vue"
import { defineComponent } from "vue"

export default defineComponent({
    props: {
        isenabled: {
            type: Boolean,
            required: true,
            default: false
        },
        typeonecount: {
            type: Number,
            required: false,
            default: 2
        },
        typetwocount: {
            type: Number,
            required: false,
            default: 2
        }
    },
    emits: {},
    setup(props, { emit }) {
        const equations = ref([])
        const userSolutions = ref([])
        const checkerObject = ref([])
        const solutionResults = ref([])
        const solutionsChecked = ref(false)
        const equationsTotal = ref(0)
        const userScore = ref(0)


        function refreshJax() {
            MathJax.typesetClear()
            MathJax.typeset()
        }

        function sendGetEqOrder(typeOneCount, typeTwoCount) {
            axios({
                method: "POST",
                url: 'https://localhost:5001/api/homeapi/getequationstosolve',
                data: {
                    "sveq": typeOneCount,
                    "hgeq": typeTwoCount
                }
            })
                .then(response => {
                    equations.value = response.data
                    console.log(response.data)
                    equationsTotal.value = equations.value.length
                    setTimeout(() => refreshJax(), 500)
                })
                .catch(function (error) {
                    console.log(error)
                })
        }
        function fillCheckerObject() {
            for (var i = 0; i < equationsTotal.value; i++) {
                checkerObject.value.push(
                    {
                        userSolution: userSolutions.value[i],
                        realSolution: equations.value[i].solution
                    })
            }
        }

        function sendSolutionsForCheck() {
            console.log("sending check")
            console.log(checkerObject.value)
            axios({
                method: "POST",
                url: "https://localhost:5001/api/homeapi/checksolutions",
                data: {
                    "solutions": checkerObject.value
                }
            })
                .then(response => {
                    console.log("got response")
                    console.log(response.data)
                    solutionResults.value = response.data
                })
                .catch(error => {
                    console.log(error)
                })
        }

        function checkButtonPress() {
            userScore.value = 0
            fillCheckerObject()
            sendSolutionsForCheck()
            for (var i = 0; i < solutionResults.value.length; i++) {
                if (solutionResults[i].value == true) {
                    userScore.value++
                }
            }
            solutionsChecked.value = true
        }

        function resetControlsState() {
            solutionsChecked.value = false
            userScore.value = 0          
            while (userSolutions.value.length > 0) {
                userSolutions.value.pop()
            }
            while (solutionResults.value.length > 0) {
                solutionResults.value.pop()
            }
            while (checkerObject.value.length > 0) {
                checkerObject.value.pop()
            }
        }

        function isPressedWatcherCheck() {
            resetControlsState()
            if (props.isenabled) {
                sendGetEqOrder(props.typeonecount, props.typetwocount)
            }
        }
        watch(() => props.isenabled, isPressedWatcherCheck)

        return {
            refreshJax,
            sendGetEqOrder,
            equations,
            checkButtonPress,
            solutionResults,
            userScore,
            solutionsChecked,
            equationsTotal,
            userSolutions
        }
    },
    template: "#test-template"
})