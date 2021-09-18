import axios from "axios";
import { defineComponent, onMounted, ref, reactive, toRefs } from "vue"
import { useGetEquationCount } from "../EquationCountModule"

export default defineComponent({
    props: {
        ispressed: {
            type: Boolean,
            required: true,
            default: false
        }
    },
    emits: {

    },
    setup(props, { emit }) {
        const sveq = ref(0)
        const hgeq = ref(0)
        const errors = ref([])

        const { equationCount, getEquationCount } = useGetEquationCount()
        onMounted(getEquationCount)

        function validateAndSend() {
            while (errors.value.length > 0) {
                errors.value.pop()
            }
            if (sveq.value + hgeq.value == 0) {
                errors.value.push("Equaton total count must be greater than 0")
            }
            if (!errors.value.length) {
                sendGenOrder()
            }
        }

        function sendGenOrder() {
            axios({
                method: 'POST',
                url: 'https://localhost:5001/api/homeapi/generateequationorder',
                data: {
                    "sveq": sveq.value,
                    "hgeq": hgeq.value
                }
            })
                .then(response => {
                    console.log(response)
                    getEquationCount()
                })
                .catch(error => {
                    console.log(error)
                });
        }

        return {
            sveq,
            hgeq,
            errors,
            validateAndSend,
            ...toRefs(equationCount)
        }
    },
    template: "#generation-template"
})