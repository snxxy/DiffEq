import { defineComponent, onMounted, ref, toRefs, watch } from "vue"
import { useGetEquationCount } from "../EquationCountModule"
import TestComponent from "./TestComponent"

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
        const isTestStarted = ref(false)
        const isSetupEnabled = ref(false)

        const { equationCount, getEquationCount } = useGetEquationCount()

        onMounted(getEquationCount)

        function validateAndSend() {
            while (errors.value.length > 0) {
                errors.value.pop()
            }
            if (sveq.value + hgeq.value == 0 || sveq.value + hgeq.value > 10) {
                errors.value.push("Equaton total count must be between 0 and 10")
            }
            if (!errors.value.length) {
                isTestStarted.value = true
            }
        }

        function isPressedSwitchWatcher() {
            if (props.ispressed) {
                isTestStarted.value = false
                isSetupEnabled.value = true
            }
            else {
                isSetupEnabled.value = false
                isTestStarted.value = false
            }
        }

        watch(() => props.ispressed, isPressedSwitchWatcher)

        return {
            sveq,
            hgeq,
            errors,
            validateAndSend,
            isTestStarted,
            isSetupEnabled,
            ...toRefs(equationCount),
        }
    },

    components: {
        TestComponent
    },
    template: "#custom-test-template"
})