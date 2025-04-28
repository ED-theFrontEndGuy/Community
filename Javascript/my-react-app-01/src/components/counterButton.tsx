interface ICounterProps {
    label: string;
    separator?: string;
}

const CounterButton = (props: ICounterProps = { label:'', separator:':' }) => (
    <button>{props.label}{props.separator} 0</button>

);

export default CounterButton;