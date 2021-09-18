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
        const userScore = ref(0)
        const solutionResults = ref([])
        const solutionsChecked = ref(false)
        const equationsTotal = ref(0)

        function refreshJax() {
            MathJax.typesetClear()
            MathJax.typeset()
        }

        function sendGetEqOrder(typeOneCount, typeTwoCount) {
            axios({
                method: 'POST',
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


        function checkButtonPress() {
            solutionsChecked.value = true
            userScore.value = 0
            for (var i = 0; i < equations.value.length; i++) {
                if (equations.value[i].solution == userSolutions.value[i]) {
                    userScore.value++
                    solutionResults.value[i] = "Ok"
                }
                else {
                    solutionResults.value[i] = "Wrong"
                }
            }
        }

        function resetControlsState() {
            solutionsChecked.value = false
            while (userSolutions.value.length > 0) {
                userSolutions.value.pop()
            }
            while (solutionResults.value.length > 0) {
                solutionResults.value.pop()
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