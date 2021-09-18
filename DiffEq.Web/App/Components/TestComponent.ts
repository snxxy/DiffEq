import axios from "axios"
import MathJax from "MathJax"
import { ref, watch } from "vue"
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
                    setTimeout(() => refreshJax(), 500)
                })
                .catch(function (error) {
                    console.log(error)
                })
        }

        function isPressedWatcherCheck() {
            if (props.isenabled) {
                sendGetEqOrder(props.typeonecount, props.typetwocount)
            }
        }
        watch(() => props.isenabled, isPressedWatcherCheck)

        return {
            refreshJax,
            sendGetEqOrder,
            equations
        }
    },
    template: "#test-template"
})