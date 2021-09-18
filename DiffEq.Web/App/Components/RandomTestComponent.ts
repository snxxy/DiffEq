import { defineComponent, ref, watch } from 'vue'
import TestComponent from './TestComponent'

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
        const isRandomTestEnabled = ref(false)

        function isPressedWatcherCheck() {
            isRandomTestEnabled.value = props.ispressed
        }

        watch(() => props.ispressed, isPressedWatcherCheck)

        return {
            isRandomTestEnabled
        }
    },
    components: {
        TestComponent,
    },
    template: "#random-test-template"
})